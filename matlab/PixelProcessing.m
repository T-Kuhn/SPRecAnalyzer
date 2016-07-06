%% 画像データを読み込む %%
imgOriginal = imread('img/SmallP60400FL.bmp', 'bmp'); % load the original image
img = double(imgOriginal);  % 型をdoubleにキャストする
imgWidth = length(img(1,:,1));
imgHeight = length(img(:,1,1));


%% greyscaleに変換 %%
greyImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
greyImg = greyImg / max(greyImg(:)); % 範囲を0～1.0にしておく
imgNmbrOfPixels = length(greyImg(:));

%% ２値化 %%
threshold = 0.3;
TwoValImg = uint16(zeros(imgHeight, imgWidth));
for pixel = 1:imgNmbrOfPixels   % loop through all the pixels. The Pixels values go from 0 to 255*3
    if greyImg(pixel) > threshold
        TwoValImg(pixel) = 1;
    else
        TwoValImg(pixel) = 0;
    end
end

%% 元の画像データを表示 %%
figure(1);
imagesc(imgOriginal); 
axis image;

%% greyscale画像データを表示 %%
figure(2);
imagesc(greyImg); colormap(gray); % display the image in matlab
axis image;

%% 二値化画像データを表示 %%
figure(3);
imagesc(TwoValImg); colormap(gray); % display the image in matlab
axis image;
