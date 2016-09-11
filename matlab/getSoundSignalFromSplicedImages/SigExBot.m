% - - - - - - - - - - - - - - - - 
% - - -  SIG EX Bot CLASS - - - -
% - - - - - - - - - - - - - - - -
classdef SigExBot < handle
    properties
        IsOnTrack;
        FirstCycle;
        TrackFollowing;
        StepSize;
        StartStepSize;
        CurrentX;
        CurrentY;
        CurrentRoundNmbr;
        Img = {};
        ImgWidth;
        ImgHeight;
        PixelThreshold; % 0 ~ 255
        CurrentImgNmbr;
        MaxImgNmbr;
        MaxImgNmbrFirstImgSet;
        MaxRoundNmbr;
        SignalIndex;
        ProcessedSignal = [];
        Signal = [];
        DebugSignal1 = [];
        SignalWidthArr = [];
        MeanSignalWidth;
        ChangeInSigWidth;
        StrangeThingCounter;
        Debug;
        AlgoStopHeight;
        CorVal;
        CurrentCorVal;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = SigExBot()
            obj.TrackFollowing = false;
            obj.FirstCycle = false;
            obj.IsOnTrack = false;      % When the Bot is created, it isn't on track
            obj.StartStepSize = 2.5;           % Default StepSize is 2 Pixel on the X Axis!
            obj.CurrentX = 1;
            obj.CurrentY = 1;
            obj.ImgWidth = 0;
            obj.ImgHeight = 0;
            obj.PixelThreshold = 250;   % Default Threshold for Pixelregocnition is 250
            obj.CurrentImgNmbr = 1;
            obj.CurrentRoundNmbr = 1;
            obj.StrangeThingCounter = 0;
            obj.SignalIndex = 0;
            obj.Signal(300000) = 0;    % Get some memory for the Signal Array
            obj.ProcessedSignal(300000) = 0;    % Get some memory for the Signal Array
            obj.Debug = true;          % Debug is On by defautlt
            obj.AlgoStopHeight = 2000; % Algorithm stops when higher than 2000px at 1st image
            obj.CorVal = 1500;         % 1500px correction after one full Round
            obj.CurrentCorVal = 0;
            obj.GetNmbrOfFolders();
            obj.GetNmbrOfFiles();
            obj.LoadImagesIntoRAM(); 
            obj.ChangeCurrentImage(obj.CurrentImgNmbr);
            obj.SetCurrentStepSize();
        end 
        % - - - - - - - - - - - - - - - - 
        % - - Get Number Of folders - - -
        % - - - - - - - - - - - - - - - -
        function GetNmbrOfFolders(obj)
            D = dir(['.', '\Round*']);
            obj.MaxRoundNmbr = length(D([D.isdir]));
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Get Number Of files - - -
        % - - - - - - - - - - - - - - - -
        function GetNmbrOfFiles (obj)
            D = dir([sprintf('Round%d/splicedImages', obj.CurrentRoundNmbr), '\*.bmp']);
            obj.MaxImgNmbr = length(D(not([D.isdir])))
            if ~obj.TrackFollowing;
                obj.MaxImgNmbrFirstImgSet = obj.MaxImgNmbr;
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Load Images into RAM  - - -
        % - - - - - - - - - - - - - - - -
        function LoadImagesIntoRAM(obj)
            for p = 1 : obj.MaxImgNmbr;  
                obj.Img(p) = {imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p), 'bmp')};
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Set Current StepSize  - -
        % - - - - - - - - - - - - - - - -
        function SetCurrentStepSize(obj)
            obj.StepSize = obj.StartStepSize * obj.MaxImgNmbr / obj.MaxImgNmbrFirstImgSet; 
        end
        % - - - - - - - - - - - - - - - - 
        % - - Change Current Image  - - -
        % - - - - - - - - - - - - - - - -
        function ChangeCurrentImage(obj, nmbr)
            if obj.Debug & obj.TrackFollowing;
                disp(sprintf('processing Round: %d Image: %d', obj.CurrentRoundNmbr, obj.CurrentImgNmbr));
            end
            tmpSize = size(obj.Img{nmbr});
            obj.ImgHeight = tmpSize(1);
            obj.ImgWidth = tmpSize(2);
            if nmbr == 1 & (obj.ImgHeight - obj.AlgoStopHeight) > obj.CurrentY & obj.TrackFollowing;
                obj.SaveMarkedImages();
                obj.CurrentRoundNmbr = obj.CurrentRoundNmbr + 1;
                obj.CurrentCorVal = obj.CurrentCorVal - obj.CorVal;
                obj.CurrentY = obj.CurrentY + obj.CorVal;
                obj.GetNmbrOfFiles();
                obj.LoadImagesIntoRAM();
                obj.SetCurrentStepSize();
                if obj.CurrentRoundNmbr > obj.MaxRoundNmbr;
                    obj.TrackFollowing = false;
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - Next Step - - - - - -
        % - - - - - - - - - - - - - - - -
        function NextStep(obj)
            obj.CurrentX = obj.CurrentX + obj.StepSize;
            obj.SignalIndex = obj.SignalIndex + 1;
            obj.Signal(obj.SignalIndex) = obj.CurrentY + obj.CurrentCorVal;
            obj.DebugSignal1(obj.SignalIndex) = obj.ChangeInSigWidth;
            if round(obj.CurrentX) > obj.ImgWidth;
                obj.CurrentX = 1;
                obj.CurrentImgNmbr = obj.CurrentImgNmbr + 1;
                if obj.CurrentImgNmbr > obj.MaxImgNmbr;
                    obj.CurrentImgNmbr = 1;
                end
                obj.ChangeCurrentImage(obj.CurrentImgNmbr)
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - Follow Track  - - - - -
        % - - - - - - - - - - - - - - - -
        function FollowTrack(obj)
            while obj.TrackFollowing;
                obj.CenterOnCurrentPos();
                obj.LeaveBlackMark();
                obj.NextStep();
                if obj.FirstCycle;
                    obj.FirstCycle = false;
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Start - - - - - - -
        % - - - - - - - - - - - - - - - -
        function Start(obj)
            obj.TrackFollowing = true;
            obj.FirstCycle = true;
            if not(obj.IsOnTrack);
                if not(obj.SearchClosestTrack());
                    'could not find closest Track!'
                    return
                else
                    obj.FollowTrack();
                end
            else
                obj.FollowTrack();
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - LeaveBlackMark  - - - -
        % - - - - - - - - - - - - - - - -
        function LeaveBlackMark(obj)
            obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX)) = 0;
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Save Marked Imgages - - -
        % - - - - - - - - - - - - - - - -
        function SaveMarkedImages(obj)
            obj
            if obj.Debug;
                disp('saving marked images');
            end
            for p = 1 : obj.MaxImgNmbr;  
                imwrite(obj.Img{p}, sprintf('Round%d/markedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p)); 
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Center On Current Pos - - -
        % - - - - - - - - - - - - - - - -
        function CenterOnCurrentPos(obj)
            if obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX)) >= obj.PixelThreshold;
                obj.StrangeThingCounter = 0;
                % How far up can we go?
                % - - - - - - - - - - - -
                incrementerUp = 0;
                while(obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY) + incrementerUp, round(obj.CurrentX)) >= obj.PixelThreshold)
                    incrementerUp = incrementerUp - 1;
                end
                % How far down can we go?
                % - - - - - - - - - - - -
                incrementerDown = 0;
                while(obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY) + incrementerDown, round(obj.CurrentX)) >= obj.PixelThreshold)
                    incrementerDown = incrementerDown + 1;
                end

                signalWidth = incrementerDown - incrementerUp;
                obj.CalcMeanSignalWidth(signalWidth);

                changeInY = (incrementerUp + incrementerDown)/2;
                
                % make it so the max chaneInY is 1
                if abs(changeInY) >= 1
                    changeInY = changeInY/abs(changeInY);
                end
                if obj.ChangeInSigWidth >= 5
                    % IF WE THIS HAPPENS WE NEED TO PROBE FOR END OF GAP!!!
                    changeInY = 0;
                end
                obj.CurrentY = obj.CurrentY + changeInY;                    

            else
                % just go straight. Somethings strange happened!     
                obj.StrangeThingCounter = obj.StrangeThingCounter + 1;
                if obj.StrangeThingCounter > 100;
                    obj.SaveMarkedImages();
                    disp('Something strange happened!');
                    obj.CurrentImgNmbr
                    obj.TrackFollowing = false;
                end 
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - -  Probe For End Of Gap - - -
        % - - - - - - - - - - - - - - - -
        function ProbeForEndOfGap(obj)
            
        end
        % - - - - - - - - - - - - - - - - 
        % - - Calc Mean Signal Width  - -
        % - - - - - - - - - - - - - - - -
        function CalcMeanSignalWidth(obj, sigWidth)
            if obj.FirstCycle;
                obj.MeanSignalWidth = sigWidth;
                obj.ChangeInSigWidth = 0;
            else
                diff = sigWidth - obj.MeanSignalWidth;
                if abs(diff) <= 8
                    obj.MeanSignalWidth = obj.MeanSignalWidth + diff/100;
                end
                obj.ChangeInSigWidth = abs(diff); 
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Search closest Track  - - -
        % - - - - - - - - - - - - - - - -
        function ret = SearchClosestTrack(obj)
            for Ypixel = obj.ImgHeight : -1 : 1;    
                if obj.Img{obj.CurrentImgNmbr}(Ypixel, 1) >= obj.PixelThreshold
                    if obj.PixelThreshold <= sum(sum(obj.Img{obj.CurrentImgNmbr}(Ypixel-15:Ypixel-5, 1:100)) / 1000);
                        obj.IsOnTrack = true;
                        obj.CurrentY = Ypixel;
                        ret = true; % Track found!          
                        return;
                    end
                end
            end
            ret = false; % No track found!
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - Process Signal  - - - -
        % - - - - - - - - - - - - - - - -
        function ProcessSignal(obj)
            for entry = obj.Signal;
                entry
            end
        end
    end
end


% - - - - - - - - - - - - - - - - 
% - - - - - - MEMO  - - - - - - -
% - - - - - - - - - - - - - - - -

% WHEN the widths changes:
%   search ahead straight for the place where things get back to normal for longer than 30 steps.







