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
        ImgRGB = {};
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
        GapFlag;
        GapFillingFlag;
        GapEndedCounter;
        GapSlope;
        StartOfGapX;
        StartOfGapY;
        StartOfGapIndex;
        StartOfGapImgNmbr;
        EndOfGapImgNmbr;
        EndOfGapIndex;
        EndOfGapY;
        ReturnIndex;
        ReturnX;
        ReturnY;
        TryToRecover;
        TryToRecoverCounter;
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
            obj.CurrentY = 2400;
            obj.ImgWidth = 0;
            obj.ImgHeight = 0;
            obj.PixelThreshold = 240;   % Default Threshold for Pixelregocnition is 250
            obj.CurrentImgNmbr = 1;
            obj.CurrentRoundNmbr = 1;
            obj.StrangeThingCounter = 0;
            obj.SignalIndex = 0;
            obj.Signal(300000) = 0;    % Get some memory for the Signal Array
            obj.ProcessedSignal(300000) = 0;    % Get some memory for the Signal Array
            obj.Debug = true;          % Debug is On by defautlt
            obj.AlgoStopHeight = 2000; % Algorithm stops when higher than 2000px at 1st image
            obj.CorVal = 1486;         % 1486px correction after one full Round
            obj.CurrentCorVal = 0;
            obj.GapEndedCounter = 0;
            obj.GapFlag = false;
            obj.GapFillingFlag = false;
            obj.TryToRecover = false;
            obj.TryToRecoverCounter = 0;
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
                tmp = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p), 'bmp');
                obj.Img(p) = {tmp};
                [m n r] = size(tmp);
                tmpRGB = uint8(zeros(m,n,3)); 
                tmpRGB(:,:,1) = uint8(tmp);
                tmpRGB(:,:,2) = tmpRGB(:,:,1);
                tmpRGB(:,:,3) = tmpRGB(:,:,1);
                obj.ImgRGB(p) = {tmpRGB};
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Set Current StepSize  - -
        % - - - - - - - - - - - - - - - -
        function SetCurrentStepSize(obj)
            obj.StepSize = obj.StartStepSize * obj.MaxImgNmbr / obj.MaxImgNmbrFirstImgSet; 
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - Go Back Images  - - - -
        % - - - - - - - - - - - - - - - -
        function GoBackImages(obj, nmbr)
            tmp = obj.CurrentImgNmbr + nmbr;
            if tmp < 1;
                obj.CurrentImgNmbr = obj.MaxImgNmbr + tmp;
            else
                obj.CurrentImgNmbr = tmp;
            end 
            if obj.Debug & obj.TrackFollowing;
                disp(sprintf('Going Back one Image to: %d Image: %d', obj.CurrentRoundNmbr, obj.CurrentImgNmbr));
            end
            tmpSize = size(obj.Img{obj.CurrentImgNmbr});
            obj.ImgHeight = tmpSize(1);
            obj.ImgWidth = tmpSize(2);
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
            if (nmbr == 1 & (obj.ImgHeight - obj.AlgoStopHeight) > obj.CurrentY & obj.TrackFollowing) | ...
                (obj.CurrentRoundNmbr == obj.MaxRoundNmbr & (obj.ImgHeight - obj.AlgoStopHeight) > obj.CurrentY); 
                obj.GapFlag = false;        % even if there is a gap which the program tries to fill, stop it!
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
            if obj.Debug;
                if obj.GapFillingFlag
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 1) = 0;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 2) = 0;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 3) = 255;
                elseif obj.GapFlag
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 1) = 255;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 2) = 0;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 3) = 0;
                else 
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 1) = 0;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 2) = 255;
                    obj.ImgRGB{obj.CurrentImgNmbr}(round(obj.CurrentY), round(obj.CurrentX), 3) = 0;
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Save Marked Imgages - - -
        % - - - - - - - - - - - - - - - -
        function SaveMarkedImages(obj)
            obj
            if obj.Debug;
                disp('saving marked images');
                for p = 1 : obj.MaxImgNmbr;  
                    imwrite(obj.ImgRGB{p}, sprintf('Round%d/markedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p)); 
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Get Centering Correction  -
        % - - - - - - - - - - - - - - - -
        function val = GetCenterOfTrack(obj)
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

            val = (incrementerUp + incrementerDown)/2;
        end
        % - - - - - - - - - - - - - - - - 
        % - - Center On Current Pos - - -
        % - - - - - - - - - - - - - - - -
        function CenterOnCurrentPos(obj)
            if obj.GapFillingFlag == true;
                obj.GapFilling();
            else
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
                    if abs(changeInY) >= 1;
                        changeInY = changeInY/abs(changeInY);
                    end

                    obj.ResetTryToRecoverFlag();

                    if obj.ChangeInSigWidth >= 5 & ~obj.TryToRecover;
                        if ~obj.GapFlag
                            obj.GapFlag = true;
                            obj.StartOfGapX = obj.CurrentX -4 * obj.StepSize;
                            obj.StartOfGapIndex = obj.SignalIndex - 4; 
                            if obj.StartOfGapX < 1;
                                obj.StartOfGapX = obj.CurrentX;
                                obj.StartOfGapIndex = obj.SignalIndex; 
                            end
                            obj.StartOfGapY = obj.Signal(obj.StartOfGapIndex) - obj.CurrentCorVal;
                            obj.StartOfGapImgNmbr = obj.CurrentImgNmbr;
                        end
                        obj.GapEndedCounter = 0;
                        changeInY = 0;
                    elseif obj.GapFlag;
                        obj.GapEndedCounter = obj.GapEndedCounter + 1;
                        if obj.GapEndedCounter > 30;
                            obj.GapFlag = false;
                            obj.GapEndedCounter = 0;
                            obj.EndOfGapIndex = obj.SignalIndex - 26;
                            obj.ReturnIndex = obj.SignalIndex;
                            obj.ReturnX = obj.CurrentX;
                            obj.ReturnY = obj.CurrentY;
                            obj.EndOfGapY = obj.Signal(obj.EndOfGapIndex) - obj.CurrentCorVal;
                            obj.CalculateSlope();
                            obj.GapFillingFlag = true; 
                            obj.SignalIndex = obj.StartOfGapIndex;
                            obj.CurrentX = obj.StartOfGapX;
                            obj.CurrentY = obj.StartOfGapY;
                            obj.EndOfGapImgNmbr = obj.CurrentImgNmbr;
                            if ~(obj.StartOfGapImgNmbr == obj.CurrentImgNmbr);
                                % Start and End on different Images
                                obj.GoBackImages(obj.StartOfGapImgNmbr - obj.CurrentImgNmbr);
                            end
                        end
                    end
                    obj.CurrentY = obj.CurrentY + changeInY;                    
                else
                    % just go straight. Somethings strange happened!     
                    obj.StrangeThingCounter = obj.StrangeThingCounter + 1;
                    if obj.StrangeThingCounter > 600;
                        if obj.TryToRecover;
                            obj.SaveMarkedImages();
                            disp('Something strange happened!');
                            disp(obj.CurrentImgNmbr);
                            obj.TrackFollowing = false;
                        end
                        obj.StrangeThingCounter = 0;
                        disp('Try to recover!');
                        obj.GapFlag = false;
                        obj.GapFillingFlag = true;
                        obj.TryToRecover = true;
                        obj.EndOfGapIndex = obj.SignalIndex;
                        obj.EndOfGapImgNmbr = obj.CurrentImgNmbr;
                        obj.ReturnIndex = obj.SignalIndex;
                        obj.ReturnX = obj.CurrentX;
                        obj.CurrentY = obj.Signal(obj.SignalIndex -450) - obj.CurrentCorVal;
                        obj.ReturnY = obj.CurrentY;
                        obj.EndOfGapY = obj.CurrentY;
                        obj.SignalIndex = obj.SignalIndex - 450;
                        obj.CurrentX = obj.CurrentX - 450 * obj.StepSize;
                        obj.StartOfGapX = obj.CurrentX;
                        obj.StartOfGapY = obj.CurrentY;
                        obj.StartOfGapIndex = obj.SignalIndex;
                        obj.StartOfGapImgNmbr = obj.CurrentImgNmbr;
                        obj.CalculateSlope();
                        if obj.CurrentX < 1;
                            obj.GoBackImages(-1);
                            obj.StartOfGapImgNmbr = obj.CurrentImgNmbr;
                            obj.CurrentX = obj.ImgWidth + obj.CurrentX; 
                        end
                    end 
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Reset Try To Recover Flag -
        % - - - - - - - - - - - - - - - -
        function ResetTryToRecoverFlag(obj)
            if obj.TryToRecover;
                obj.TryToRecoverCounter = obj.TryToRecoverCounter + 1;
                if obj.TryToRecoverCounter > 500;
                    obj.TryToRecoverCounter = 0;
                    obj.TryToRecover = false;
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - GapFilling  - - - - -
        % - - - - - - - - - - - - - - - -
        function GapFilling(obj)
            if obj.SignalIndex < obj.EndOfGapIndex;
                % do the gap filling!
                obj.CurrentY = obj.StartOfGapY + (obj.SignalIndex - obj.StartOfGapIndex) * obj.GapSlope;
            else
                % switch back to normal behavior
                obj.GapFillingFlag = false;
                obj.SignalIndex = obj.ReturnIndex;
                obj.CurrentX = obj.ReturnX;
                obj.CurrentY = obj.ReturnY;
                obj.CurrentImgNmbr = obj.EndOfGapImgNmbr;
                obj.ChangeCurrentImage(obj.CurrentImgNmbr);
            end 
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - Calculate Slope - - - -
        % - - - - - - - - - - - - - - - -
        function CalculateSlope(obj)
            length = obj.EndOfGapIndex - obj.StartOfGapIndex;
            height = obj.EndOfGapY - obj.StartOfGapY;
            obj.GapSlope = height / length;
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
                if abs(diff) <= 18
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
%   




