% - - - - - - - - - - - - - - - - 
% - - - - - -  Buffer - - - - - -
% - - - - - - - - - - - - - - - -
classdef Buffer < handle
    properties
        BufferArr 
        BufferSize   
        NmbrOfEntries = 0;
        QIndex = 0;
        DeqIndex = 0;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = Buffer(sizeOfBuffer)
            if nargin > 0
                if isnumeric(sizeOfBuffer);
                    obj.BufferSize = sizeOfBuffer;
                    obj.BufferArr = Point.empty(sizeOfBuffer, 0);
                    obj.BufferArr(sizeOfBuffer) = Point();
                end
            else
                error('size of the Buffer must be passed as an argument');
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - - Queue - - - - - -
        % - - - - - - - - - - - - - - - -
        function queue(obj, point)
            if obj.NmbrOfEntries < obj.BufferSize;
                obj.QIndex = obj.QIndex + 1;
                if obj.QIndex > obj.BufferSize;
                    obj.QIndex = 1;
                end
                obj.NmbrOfEntries = obj.NmbrOfEntries + 1;
                obj.BufferArr(obj.QIndex) = point;
            else
                error('Buffer Overflow');
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Dequeue - - - - - -
        % - - - - - - - - - - - - - - - -
        function retPoint = dequeue(obj)
            obj.DeqIndex = obj.DeqIndex + 1;
            if obj.DeqIndex > obj.BufferSize;
                obj.DeqIndex = 1;
            end
            obj.NmbrOfEntries = obj.NmbrOfEntries - 1;
            retPoint = obj.BufferArr(obj.DeqIndex);
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - Entry Check - - - - -
        % - - - - - - - - - - - - - - - -
        function r = entryCheck(obj);
            r = obj.NmbrOfEntries >= 1;
        end
    end
end
