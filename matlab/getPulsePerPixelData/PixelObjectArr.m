% - - - - - - - - - - - - - - - - 
% - - - PixelObjectArr Class  - -
% - - - - - - - - - - - - - - - -
classdef PixelObjectArr < handle
    properties
        PixelObjects = [];
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = PixelObjectArr()
            obj.PixelObjects = PixelObject_.empty(0,0);
        end 
    end
end