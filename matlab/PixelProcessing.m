%% �摜�f�[�^��ǂݍ��� %%
img = imread('p3FL.bmp', 'bmp'); % load the original image
imgWidth = length(img(1,:,1));
imgHeight = length(img(:,1,1));


%% greyscale�ɕϊ� %%
greyImg = img(:,:,2);

imgNmbrOfPixels = length(greyImg(:));

%% �Q�l�� %%
threshold = 110;
TwoValImg = greyImg;
for pixel = 1:imgNmbrOfPixels   % loop through all the pixels. The Pixels values go from 0 to 255*3
    if greyImg(pixel) > threshold
        TwoValImg(pixel) = 1;
    else
        TwoValImg(pixel) = 0;
    end
end

%% ���̉摜�f�[�^��\�� %%
figure(1);
imagesc(img);

%% greyscale�摜�f�[�^��\�� %%
figure(2);
imagesc(greyImg); colormap(gray); % display the image in matlab

%% ��l���摜�f�[�^��\�� %%
figure(3);
imagesc(TwoValImg); colormap(gray); % display the image in matlab
