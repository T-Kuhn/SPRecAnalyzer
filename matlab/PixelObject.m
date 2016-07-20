% - - - - - - - - - - - - - - - - 
% - - - - PixelObject Class - - -
% - - - - - - - - - - - - - - - -
classdef PixelObject < handle
    properties
        PointArr
        EntryIndex = 0
        Buffer
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = PixelObject(nmbrOfEntries)
            obj.PointArr = Point.empty(nmbrOfEntries, 0);
            obj.PointArr(nmbrOfEntries) = Point();    
            obj.Buffer = Buffer(200);
        end 
        % - - - - - - - - - - - - - - - - 
        % - - - - - Add Point - - - - - -
        % - - - - - - - - - - - - - - - -
        function addPoint(obj, imgData, point)
            obj.EntryIndex = obj.EntryIndex + 1;    
            obj.PointArr(obj.EntryIndex) = point;
            imgData.MapArr(point.Y, point.X) = 0;
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Find All Neighbours - - -
        % - - - - - - - - - - - - - - - -
        function findAllNeighbours(obj, imgData, startPoint)
            obj.addPoint(imgData, startPoint); 
            
            obj.Buffer.queue(startPoint);

            % Look at all the points near the startPoint
            % if there are some true ones, add them and put them in the Buffer
            % startPoint = Buffer.dequeue();
            % end when there are no more points in the Buffer!
            while obj.Buffer.NmbrOfEntries > 0 
                tmpPoint = obj.Buffer.dequeue(); 

                % Look at the Point above the tmpPoint 
                checkPoint = Point(tmpPoint.X + 0, tmpPoint.Y + 1);
                if imgData.pointCheck(checkPoint) 
                    obj.addPoint(imgData, checkPoint);
                    obj.Buffer.queue(checkPoint);
                end
            
                % Look at the Point under the tmpPoint 
                checkPoint = Point(tmpPoint.X + 0, tmpPoint.Y -1);
                if imgData.pointCheck(checkPoint) 
                    obj.addPoint(imgData, checkPoint);
                    obj.Buffer.queue(checkPoint);
                end

                % Look at the Point on the right of the tmpPoint 
                checkPoint = Point(tmpPoint.X + 1, tmpPoint.Y + 0);
                if imgData.pointCheck(checkPoint) 
                    obj.addPoint(imgData, checkPoint);
                    obj.Buffer.queue(checkPoint);
                end
                
                % Look at the Point on the left of the tmpPoint
                checkPoint = Point(tmpPoint.X - 0, tmpPoint.Y + 0);
                if imgData.pointCheck(checkPoint) 
                    obj.addPoint(imgData, checkPoint);
                    obj.Buffer.queue(checkPoint);
                end

            end
        end
    end
end
