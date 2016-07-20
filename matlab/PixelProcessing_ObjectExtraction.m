% - - - - - - - - - - - - - - - - 
% - - - Object Extraction - - - -
% - - - - - - - - - - - - - - - -

pixelObjIndex = 0;
pixelObjArr = PixelObject.empty(2,0);
%for i = 1 : imageData.EntryIndex;
for i = 1 : 100;
    point = imageData.PointArr(i);
    if imageData.pointCheck(point)
        pixelObjIndex = pixelObjIndex + 1; 
        pixelObjArr(pixelObjIndex) = PixelObject(1000);
        pixelObjArr(pixelObjIndex).findAllNeighbours(imageData, point);
    end
end

