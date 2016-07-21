% - - - - - - - - - - - - - - - - 
% - - - Object Extraction - - - -
% - - - - - - - - - - - - - - - -
tic
pixelObjIndex = 0;
pixelObjArr = PixelObject.empty(2,0);
for i = 1 : imageData.EntryIndex;
%for i = 1 : 100;
    point = imageData.PointArr(i);
    if imageData.pointCheck(point)
        pixelObjIndex = pixelObjIndex + 1; 
        pixelObjArr(pixelObjIndex) = PixelObject(1000);
        pixelObjArr(pixelObjIndex).findAllNeighbours(imageData, point);
    end
end

toc

OEimg = uint16(zeros(imageData.Height, imageData.Width));
pixelColor = 1;
for ent = pixelObjArr;
    for pixelNmbr = 1 : ent.EntryIndex;
        OEimg(ent.PointArr(pixelNmbr).Y, ent.PointArr(pixelNmbr).X) = pixelColor;
    end
    pixelColor = pixelColor + 1;
end

figure(4);
imagesc(OEimg); colormap(jet); % display the image in matlab
axis image;


