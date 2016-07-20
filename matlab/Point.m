% - - - - - - - - - - - - - - - - 
% - - - - - Point Class - - - - -
% - - - - - - - - - - - - - - - -
classdef Point < handle
    properties
        X
        Y 
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = Point(x, y)
            if nargin == 2
                if isnumeric(x) && isnumeric(y);
                    obj.X = x;
                    obj.Y = y;
                end
            elseif nargin == 0;
                obj.X = 0;
                obj.Y = 0;
            else
                error('argument number mismatch');
            end
        end
    end
end
