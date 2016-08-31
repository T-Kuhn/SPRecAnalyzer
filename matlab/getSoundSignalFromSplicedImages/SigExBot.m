% - - - - - - - - - - - - - - - - 
% - - -  SIG EX Bot CLASS - - - -
% - - - - - - - - - - - - - - - -
classdef SigExBot < handle
    properties
        IsOnTrack;
        FirstCycle;
        TrackFollowing;
        StepSize;
        CurrentX;
        CurrentY;
        Img = {};
        ImgWidth;
        ImgHeight;
        PixelThreshold; % 0 ~ 255
        CurrentImgNmbr;
        MaxImgNmbr;
        SignalIndex;
        ProcessedSignal = [];
        Signal = [];
        SignalWidthArr = [];
        MeanSignalWidth;
        ChangeInSigWidth;
        StrangeThingCounter;
        Debug;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = SigExBot(maxNmbr)
            obj.IsOnTrack = false;      % When the Bot is created, it isn't on track
            obj.StepSize = 2;           % Default StepSize is 2 Pixel on the X Axis!
            obj.CurrentX = 1;
            obj.CurrentY = 1;
            obj.ImgWidth = 0;
            obj.ImgHeight = 0;
            obj.PixelThreshold = 250;   % Default Threshold for Pixelregocnition is 250
            obj.CurrentImgNmbr = 1;
            obj.MaxImgNmbr = maxNmbr;
            obj.StrangeThingCounter = 0;
            obj.SignalIndex = 0;
            obj.Signal(300000) = 0;    % Get some memory for the Signal Array
            obj.ProcessedSignal(300000) = 0;    % Get some memory for the Signal Array
            obj.Debug = true;          % Debug is On by defautlt
            obj.LoadImagesIntoRAM();
            obj.ChangeCurrentImage(obj.CurrentImgNmbr);
        end 
        % - - - - - - - - - - - - - - - - 
        % - - Load Images into RAM  - - -
        % - - - - - - - - - - - - - - - -
        function LoadImagesIntoRAM(obj)
            for p = 1 : obj.MaxImgNmbr;  
                obj.Img(p) = {imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp',1,p), 'bmp')};
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Change current Image  - - -
        % - - - - - - - - - - - - - - - -
        function ChangeCurrentImage(obj, nmbr)
            tmpSize = size(obj.Img{nmbr});
            obj.ImgHeight = tmpSize(1);
            obj.ImgWidth = tmpSize(2);
            if nmbr == 1 & (obj.ImgHeight - 2000) > obj.CurrentY;
                obj.SaveMarkedImages();
                obj.TrackFollowing = false;
            end
        end
        % - - - - - - - - - - - - - - 1- - 
        % - - - - - Next Step - - - - - -
        % - - - - - - - - - - - - - - - -
        function NextStep(obj)
            obj.CurrentX = obj.CurrentX + obj.StepSize;
            obj.SignalIndex = obj.SignalIndex + 1;
            obj.Signal(obj.SignalIndex) = obj.CurrentY;
            if obj.CurrentX > obj.ImgWidth;
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
            obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY), obj.CurrentX) = 0;
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Save Marked Imgages - - -
        % - - - - - - - - - - - - - - - -
        function SaveMarkedImages(obj)
            for p = 1 : obj.MaxImgNmbr;  
                imwrite(obj.Img{p}, sprintf('markedImages/splicedImage%d.bmp',p)); 
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Center On Current Pos - - -
        % - - - - - - - - - - - - - - - -
        function CenterOnCurrentPos(obj)
            if obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY), obj.CurrentX) >= obj.PixelThreshold;
                obj.StrangeThingCounter = 0;
                % How far up can we go?
                % - - - - - - - - - - - -
                incrementerUp = 0;
                while(obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY) + incrementerUp, obj.CurrentX) >= obj.PixelThreshold)
                    incrementerUp = incrementerUp - 1;
                end
                % How far down can we go?
                % - - - - - - - - - - - -
                incrementerDown = 0;
                while(obj.Img{obj.CurrentImgNmbr}(round(obj.CurrentY) + incrementerDown, obj.CurrentX) >= obj.PixelThreshold)
                    incrementerDown = incrementerDown + 1;
                end

                signalWidth = incrementerDown - incrementerUp;
                obj.CalcMeanSignalWidth(signalWidth);
                
                changeInY = (incrementerUp + incrementerDown)/2 * obj.ChangeInSigWidth;
                if abs(changeInY) >= 2 & not(obj.FirstCycle);
                    % make the change smaller.
                    obj.CurrentY = obj.CurrentY + changeInY/abs(changeInY);                    
                else
                    % Center normally
                    obj.CurrentY = obj.CurrentY + changeInY;                    
                end


            else
                % just go straight. Somethings strange happened!     
                obj.StrangeThingCounter = obj.StrangeThingCounter + 1;
                if obj.StrangeThingCounter > 100;
                    obj.SaveMarkedImages();
                    'Something strange happened!'
                    obj.CurrentImgNmbr
                    obj.TrackFollowing = false;
                end 
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Calc Mean Signal Width  - -
        % - - - - - - - - - - - - - - - -
        function CalcMeanSignalWidth(obj, sigWidth)
            if obj.FirstCycle;
                obj.MeanSignalWidth = sigWidth;
                obj.ChangeInSigWidth = 1;
            else
                diff = sigWidth - obj.MeanSignalWidth;
                obj.MeanSignalWidth = obj.MeanSignalWidth + diff/100;
                obj.ChangeInSigWidth = 1;
                if abs(diff) > 1;
                    obj.ChangeInSigWidth = 1/abs(diff); 
                end
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










