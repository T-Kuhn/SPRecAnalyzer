% - - - - - - - - - - - - - - - - 
% - - - - Needle CLASS  - - - - -
% - - - - - - - - - - - - - - - -
classdef Needle < handle
    properties
        Signal;
        Yn;
        Force;
        Mass;
        v;
        K;
        a;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = Needle(signal)
            obj.Signal = signal;
            obj.Yn = [];
            obj.Yn(length(obj.Signal)) = 0; % Here we make another array the same size as obj.Signal
            obj.Init();
        end

        % - - - - - - - - - - - - - - - - 
        % - - - - - - Init  - - - - - - -
        % - - - - - - - - - - - - - - - -
        function Init(obj)
            obj.Yn(1) = obj.Signal(1);
            obj.Mass = 10;
            obj.v = 0;
            obj.K = 0.1;
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Start - - - - - -
        % - - - - - - - - - - - - - - - -
        function Start(obj)
            for i = 2:length(obj.Signal);
                obj.Force = obj.K * (obj.Signal(i) - obj.Yn(i-1));
                obj.a = obj.Force / obj.Mass;
                obj.v = obj.v + obj.a;
                obj.Yn(i) = obj.Yn(i-1) + obj.v;            
            end 
            obj.Plot();
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Plot  - - - - - -
        % - - - - - - - - - - - - - - - -
        function Plot(obj)
            plot(obj.Yn);
            hold on;
            plot(obj.Signal);
        end
    end
end












% - We need limits!
