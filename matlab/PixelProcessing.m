% - - - - - - - - - - - - - - - - 
% - - - - Load Image Data - - - -
% - - - - - - - - - - - - - - - -
profile on
imgOriginal = imread('img/SmallP59000FL.bmp', 'bmp'); % load the original image
%imgOriginal = imread('img/P60400FL.bmp', 'bmp'); % load the original image
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
imageData = ImageDataClass(imgWidth, imgHeight);
imageData.setup(grayImg, threshold);

% - - - - - - - - - - - - - - - - 
% - - - -  Show Results - - - - -
% - - - - - - - - - - - - - - - -
figure(1);
imagesc(imgOriginal); 
axis image;

figure(2);
imagesc(grayImg); colormap(gray); % display the image in matlab
axis image;

figure(3);
imagesc(imageData.MapArr); colormap(gray); % display the image in matlab
axis image;

% - - - - - - - - - - - - - - - - 
% - - -  Pixel Processing - - - -
% - - - - - - - - - - - - - - - -
PixelProcessing_ObjectExtraction();



