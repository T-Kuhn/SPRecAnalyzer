% - - - - - - - - - - - - - - - - 
% - - - - Needle CLASS  - - - - -
% - - - - - - - - - - - - - - - -
classdef Needle < handle
    properties
        Signal;
        TrendCorrectedSignal;
        FFTOutput;
        SmoothingOutput;
        NeedleSimOut;
        SignalLength;
        Yn;
        x;
        SamplingRate;
        Drag;
        SignalLengthInSecs;
        xTime;
        %NeedleSimualtion properties
        Speed;
        Mass;
        Acceleration;
        NeedleForce;
        SpeedResForce
        SpringResForce
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = Needle(signal)
            obj.Signal = signal;
            obj.SignalLength = length(obj.Signal);
            obj.Yn = [];
            obj.Init();
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Init  - - - - - - -
        % - - - - - - - - - - - - - - - -
        function Init(obj)
            obj.Drag = 0.0001;
            obj.x = linspace(1, obj.SignalLength, obj.SignalLength);
            obj.SamplingRate = 180000;
            obj.SignalLengthInSecs = obj.SignalLength / obj.SamplingRate;
            obj.xTime = linspace(0, obj.SignalLengthInSecs, obj.SignalLength);
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Start - - - - - -
        % - - - - - - - - - - - - - - - -
        function Start(obj)
            obj.TrendCorrectSignal();
            obj.FFT();
            obj.Crop();
            obj.AdjustAmplitude();

            obj.NeedleSimulation();

            obj.Plot();
            obj.SaveWAV();
        end
        % - - - - - - - - - - - - - - - - 
        % - - -  NeedleSimulation - - - -
        % - - - - - - - - - - - - - - - -
        function NeedleSimulation(obj)
            obj.Acceleration = 0;
            obj.Speed = 0;
            obj.Mass = 2;
            obj.NeedleSimOut(length(obj.FFTOutput)) = 0; % Here we make another array the same size as the input signal 
            obj.NeedleSimOut(1) = obj.FFTOutput(1);
            for i = 2:length(obj.FFTOutput);
                % compute all the forces for the current step
                obj.NeedleForce = obj.FFTOutput(i) - obj.NeedleSimOut(i-1);
                obj.SpeedResForce = -obj.Speed*0.1;
                obj.SpringResForce = -obj.NeedleSimOut(i-1) * 0.5;

                obj.Acceleration = (obj.NeedleForce + obj.SpeedResForce + obj.SpringResForce)/obj.Mass; % a = (F + SpeedRes + SpringRes)/m
                
                obj.Speed = obj.Speed + obj.Acceleration * 0.3; % 0.01 because we don't want to add the full acceleration every time.
                obj.NeedleSimOut(i) = obj.NeedleSimOut(i-1) + obj.Speed*0.005;
     
                % First thing: The Force which tries to move the needle towards the Signal!    
                % -SignalForce
            
                % Then: All the Forces which work against the first Force!
                % -SpeedResistanceForce (the greater the moving speed the greater this Force)

                % -SpringForce (tries to move needle back to center position)
            end 
        end
        % - - - - - - - - - - - - - - - - 
        % - - - TrendCorrectSignal  - - -
        % - - - - - - - - - - - - - - - -
        function TrendCorrectSignal(obj)
            p = polyfit(obj.x, obj.Signal, 1);
            obj.TrendCorrectedSignal = obj.Signal - polyval(p, obj.x);
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - SmoothingSignal - - - -
        % - - - - - - - - - - - - - - - -
        function SmoothingSignal(obj)
            obj.Yn(length(obj.FFTOutput)) = 0; % Here we make another array the same size as the input signal 
            obj.Yn(1) = obj.FFTOutput(1);
            for i = 2:length(obj.FFTOutput);
                diff = obj.FFTOutput(i) - obj.Yn(i-1);
                obj.Yn(i) = obj.Yn(i-1) + obj.Drag * diff;            
            end 
            obj.SmoothingOutput = obj.FFTOutput - obj.Yn; 
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Plot  - - - - - -
        % - - - - - - - - - - - - - - - -
        function Plot(obj)
            figure('Name', 'FFTOutput + NeedleSimOut'); 
            plot(obj.FFTOutput);
            hold on;
            plot(obj.NeedleSimOut);
            %figure('Name', 'FFTOutput secs1'); 
            %plot(obj.xTime, obj.FFTOutput);
            %figure('Name', 'FFTOutput secs2'); 
            %plot(obj.xTime, obj.FFTOutput);
            %figure('Name', 'FFTOutput datapoints'); 
            %plot(obj.FFTOutput);
            %figure('Name', 'original Signal plot'); 
            %plot(obj.Signal);
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - Adjust Amplitude  - - -
        % - - - - - - - - - - - - - - - -
        function AdjustAmplitude(obj)
            obj.FFTOutput = obj.FFTOutput/ max(abs(obj.FFTOutput));
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - FFT - - - - - - -
        % - - - - - - - - - - - - - - - -
        function FFT(obj)
            %obj.TrendCorrectedSignal(1) = obj.TrendCorrectedSignal(length(obj.TrendCorrectedSignal));
            [obj.FFTOutput, f, y, y2] = fftf(obj.xTime, obj.TrendCorrectedSignal, 15000, 100);
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Crop  - - - - - -
        % - - - - - - - - - - - - - - - -
        function Crop(obj)
            % Crop the first and last entries of the FFTOutput
            obj.FFTOutput(1:10000) = [];
            obj.xTime(1:10000) = [];
            len = length(obj.FFTOutput);
            obj.FFTOutput(len-10000:len) = [];
            obj.xTime(len-10000:len) = [];
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Save WAV  - - - - -
        % - - - - - - - - - - - - - - - -
        function SaveWAV(obj)
           audiowrite('needleOutput.wav', obj.FFTOutput, obj.SamplingRate); 
           audiowrite('needleOutputSimuNeedle.wav', obj.NeedleSimOut, obj.SamplingRate); 
        end
    end
end


