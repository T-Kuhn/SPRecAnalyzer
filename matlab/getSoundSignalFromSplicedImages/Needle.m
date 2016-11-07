% - - - - - - - - - - - - - - - - 
% - - - - Needle CLASS  - - - - -
% - - - - - - - - - - - - - - - -
classdef Needle < handle
    properties
        Signal;
        TrendCorrectedSignal;
        FFTOutput;
        SmoothingOutput;
        SignalLength;
        Yn;
        x;
        SamplingRate;
        Drag;
        SignalLengthInSecs;
        xTime;
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
            %obj.SmoothingSignal();
            obj.Crop();
            obj.AdjustAmplitude();
            obj.Plot();
            obj.SaveWAV();
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
            figure('Name', 'FFTOutput'); 
            plot(obj.xTime, obj.FFTOutput);
            figure('Name', 'original Signal plot'); 
            plot(obj.Signal);
            figure('Name', 'comparing TrendCorrected Signal to FFTOutput'); 
            plot(obj.TrendCorrectedSignal);
            hold on;
            plot(obj.FFTOutput);
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
            [obj.FFTOutput, f, y, y2] = fftf(obj.xTime, obj.TrendCorrectedSignal, 15000, 20);
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
        end
    end
end












% - We need limits!
