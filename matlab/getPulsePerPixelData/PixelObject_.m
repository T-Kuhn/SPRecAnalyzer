% - - - - - - - - - - - - - - - - 
% - - - - PixelObject Class - - -
% - - - - - - - - - - - - - - - -
classdef PixelObject_ < handle
    properties
        PointArr = [[], []];
        ID = 0
        EntryIndex = 0
        CenterPos = [0, 0]
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = PixelObject_(id)
            obj.ID = id;
        end 
        % - - - - - - - - - - - - - - - - 
        % - - - - - Add Point - - - - - -
        % - - - - - - - - - - - - - - - -
        function addPoint(obj, point)
            obj.EntryIndex = obj.EntryIndex + 1;    
            obj.PointArr(obj.EntryIndex, 1) = point(1);
            obj.PointArr(obj.EntryIndex, 2) = point(2);
        end
    end
end
