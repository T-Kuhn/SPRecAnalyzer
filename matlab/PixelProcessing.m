%% �摜�f�[�^��ǂݍ��� %%
imgOriginal = imread('img/SmallP60400FL.bmp', 'bmp'); % load the original image
img = double(imgOriginal);  % �^��double�ɃL���X�g����
imgWidth = length(img(1,:,1));
imgHeight = length(img(:,1,1));


%% greyscale�ɕϊ� %%
greyImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
greyImg = greyImg / max(greyImg(:)); % �͈͂�0�`1.0�ɂ��Ă���
imgNmbrOfPixels = length(greyImg(:));

%% �Q�l�� %%
threshold = 0.3;
TwoValImg = uint16(zeros(imgHeight, imgWidth));
for pixel = 1:imgNmbrOfPixels   % loop through all the pixels. The Pixels values go from 0 to 255*3
    if greyImg(pixel) > threshold
        TwoValImg(pixel) = 1;
    else
        TwoValImg(pixel) = 0;
    end
end

%% ���̉摜�f�[�^��\�� %%
figure(1);
imagesc(imgOriginal); 
axis image;

%% greyscale�摜�f�[�^��\�� %%
figure(2);
imagesc(greyImg); colormap(gray); % display the image in matlab
axis image;

%% ��l���摜�f�[�^��\�� %%
figure(3);
imagesc(TwoValImg); colormap(gray); % display the image in matlab
axis image;
