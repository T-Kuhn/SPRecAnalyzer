% - - - - - - - - - - - - - - - - 
% - - - - Load Image Data - - - -
% - - - - - - - - - - - - - - - -
tic
imgOriginal = imread('img/tmp.bmp', 'bmp'); % load the original image
%imgOriginal = imread('img/big.bmp', 'bmp'); % load the original image
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
for pixel = 1:imgHeight*imgWidth   % loop through all the pixels. The Pixels values go from 0 to 255*3
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
imagesc(grayImg); colormap(gray); % display the image in matlab
axis image;

figure(3);
imagesc(TwoValImg); colormap(gray); % display the image in matlab
axis image;

% - - - - - - - - - - - - - - - - 
% - - -  Pixel Processing - - - -
% - - - - - - - - - - - - - - - -

X = TwoValImg;
[m,n] = size(X);

% - - - - - - - - - - - - - - - - 
% - - - DELETE FRAME PIXELS - - -
% - - - - - - - - - - - - - - - -
X(1,:) = 0;
X(m,:) = 0;
X(:,1) = 0;
X(:,n) = 0;

% - - - - - - - - - - - - - - - - 
% - - - - - - LABELING  - - - - -
% - - - - - - - - - - - - - - - -
label = 0;
for kx = 2:m-1;
    for ky = 2:n-1;
        W = zeros(3);
        W(1:3,1:3) = X(kx-1:kx+1, ky-1:ky+1);
        maxW = max(W(:));
        if X(kx,ky) == 1 && X(kx,ky-1) == 0 && maxW == 1;
            label = label + 1;
            X(kx,ky) = label;
        end
        if X(kx,ky) == 1 && maxW >= 1;
            X(kx,ky) = maxW;
        end
    end
end
figure(4);
imagesc(X); axis image;
colormap(gray);

% - - - - - - - - - - - - - - - - 
% - DELETE OVERLAPPING LABELS - -
% - - - - - - - - - - - - - - - -
while 1
    kk = 0;
    for kx = 2:m-1;
        for ky = 2:n-1;
            if X(kx,ky)~= 0 && X(kx+1,ky)~= 0 && X(kx,ky)~= X(kx+1,ky)
                old = X(kx,ky); new = X(kx+1,ky); kk = 1; break;
            end
        end
    end
    if kk == 0
        break;
    end
    for kx = 2:m-1;
        for ky = 2:n-1;  
            if X(kx,ky) == old;
                X(kx,ky) = new;
            end
        end
    end
end
toc
figure(5);
imagesc(X); axis image

% - - - - - - - - - - - - - - - - 
% - - - OBJECT EXTRACTION - - - -
% - - - - - - - - - - - - - - - -

indexCounter = 0;
existingPixelObjects = [0];
PixelObjArr = PixelObjectArr();
map = [];
for kx = 2:m-1;
    for ky = 2:n-1;
        tmp = X(kx, ky);
        if tmp ~= 0;
            ex = false;
            for ent = existingPixelObjects;
                if ent == tmp;
                    ex = true;
                    break;    
                end 
            end
            if ex ~= 0;
                % The thing already exists!
                PixelObjArr.PixelObjects(map(tmp)).addPoint([kx, ky]);
            else
                % The thing didn't exist!
                indexCounter = indexCounter + 1;
                existingPixelObjects(indexCounter) = tmp;            
                % Here we need to add another entry to the map!
                map(tmp) = indexCounter;
                PixelObjArr.PixelObjects(indexCounter) = PixelObject_(indexCounter);
                PixelObjArr.PixelObjects(indexCounter).addPoint([kx, ky]);
            end
        end
    end
end

% - - - - - - - - - - - - - - - - 
% -  FILTER WITH PIXELTHRESHOLD -
% - - - - - - - - - - - - - - - -

PixelObjArrFiltered = PixelObjectArr();
pixelThreshold = 1000;
indexCounter = 0;
for ent = PixelObjArr.PixelObjects;
    if ent.EntryIndex > pixelThreshold;
        indexCounter = indexCounter + 1;
        PixelObjArrFiltered.PixelObjects(indexCounter) = ent;
    end
end

% - - - - - - - - - - - - - - - - 
% - - CALCULATE CENTERPOINTS  - -
% - - - - - - - - - - - - - - - -

for ent = PixelObjArrFiltered.PixelObjects;
    totalX = sum(ent.PointArr(:,1));
    ent.CenterPos(1) = uint16(totalX/ent.EntryIndex);
    totalY = sum(ent.PointArr(:,2));
    ent.CenterPos(2) = uint16(totalY/ent.EntryIndex);
end

savejson('PixelObjectArr',PixelObjArrFiltered,'PixelObjectArr.json');









