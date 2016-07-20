% - - - - - - - - - - - - - - - - 
% - - - - Image Data Class  - - -
% - - - - - - - - - - - - - - - -
classdef ImageDataClass < handle
    properties
        Width
        Height
        PointArr
        MapArr
        EntryIndex = 0;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = ImageDataClass(width, height)
            obj.Width = width;
            obj.Height = height;
            obj.PointArr = Point.empty(width*height/2, 0);
            obj.PointArr(width*height/2) = Point();
            obj.MapArr = false(height, width);
        end 
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Setup - - - - - - -
        % - - - - - - - - - - - - - - - -
        function setup(obj, grayImg, threshold)
            for x = 1 : obj.Width; 
                for y = 1 : obj.Height;
                    if grayImg(y, x) > threshold 
                        obj.EntryIndex = obj.EntryIndex + 1;    
                        obj.PointArr(obj.EntryIndex) = Point(x,y);
                        obj.MapArr(y, x) = true;
                    end
                end
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - pointCheck  - - - - -
        % - - - - - - - - - - - - - - - -
        function r = pointCheck(obj, point)
            if point.X >= 1 && point.X <= obj.Width && point.Y >= 1 && point.Y <= obj.Height;
                if obj.MapArr(point.Y, point.X);
                    r = true;
                else
                    r = false;
                end
            end
        end
    end
end
