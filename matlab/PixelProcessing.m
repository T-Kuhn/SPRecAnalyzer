% - - - - - - - - - - - - - - - - 
% - - - - Load Image Data - - - -
% - - - - - - - - - - - - - - - -
tic
% load the original image
imgOriginal = imread('img/SmallP59000FL.bmp', 'bmp'); 
% load the original image
%imgOriginal = imread('img/P60400FL.bmp', 'bmp'); 
img = uint16(imgOriginal); 
[imgHeight, imgWidth] = size(img(:,:,1));
% - - - - - - - - - - - - - - - - 
% - - -  GrayScale Bitmap - - - -
% - - - - - - - - - - - - - - - -
grayImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
grayImg = grayImg / 3;
imgNmbrOfPixels = length(grayImg(:));

% - - - - - - - - - - - - - - - - 
% - - - - create TwoValImg  - - -
% - - - - - - - - - - - - - - - -
threshold = 80;
TwoValImg = uint16(zeros(imgHeight, imgWidth));
% loop through all the pixels.
% The Pixels values go from 0 to 255*3
for pixel = 1:imgHeight*imgWidth   
    if grayImg(pixel) > threshold
        TwoValImg(pixel) = 1;
    else
        TwoValImg(pixel) = 0;
    end
end


%imageData = ImageDataClass(imgWidth, imgHeight);
%imageData.setup(grayImg, threshold);

% - - - - - - - - - - - - - - - - 
% - - - -  Show Results - - - - -
% - - - - - - - - - - - - - - - -
figure(1);
imagesc(imgOriginal); 
axis image;

figure(2);
% display the image in matlab
imagesc(grayImg); colormap(gray); 
axis image;

figure(3);
% display the image in matlab
imagesc(TwoValImg); colormap(gray); 
axis image;

% - - - - - - - - - - - - - - - - 
% - - -  Pixel Processing - - - -
% - - - - - - - - - - - - - - - -
ObjectExtraction();



