% - - - - - - - - - - - - - - - - 
% - - CONVERT TO GRAYSCALE  - - -
% - - - - - - - - - - - - - - - -
for p = 0 : 205;
    img = imread(sprintf('splicedImages/splicedImage%d.bmp',p), 'bmp');
    % - - - - - - - - - - - - - - - - 
    % - - -  GrayScale Bitmap - - - -
    % - - - - - - - - - - - - - - - -
    grayImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
    grayImg = double(grayImg);
    grayImg = grayImg / max(grayImg(:));
    imgNmbrOfPixels = length(grayImg(:));
    p 
    imwrite(grayImg,sprintf('splicedImagesGrayscale/splicedImage%d.bmp',p));
end
