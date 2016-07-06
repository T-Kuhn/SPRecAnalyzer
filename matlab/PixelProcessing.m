%% 画像データを読み込む %%
img = imread('p3FL.bmp', 'bmp'); % load the original image
imgWidth = length(img(1,:,1));
imgHeight = length(img(:,1,1));


%% greyscaleに変換 %%
greyImg = img(:,:,2);

imgNmbrOfPixels = length(greyImg(:));

%% ２値化 %%
threshold = 110;
TwoValImg = greyImg;
for pixel = 1:imgNmbrOfPixels   % loop through all the pixels. The Pixels values go from 0 to 255*3
    if greyImg(pixel) > threshold
        TwoValImg(pixel) = 1;
    else
        TwoValImg(pixel) = 0;
    end
end

%% 元の画像データを表示 %%
figure(1);
imagesc(img);

%% greyscale画像データを表示 %%
figure(2);
imagesc(greyImg); colormap(gray); % display the image in matlab

%% 二値化画像データを表示 %%
figure(3);
imagesc(TwoValImg); colormap(gray); % display the image in matlab
