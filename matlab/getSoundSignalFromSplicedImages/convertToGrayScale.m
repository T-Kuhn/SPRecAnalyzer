function convertToGrayScale(x, y)
    roundNmbr = x;
    splicedImages = y; 

    % - - - - - - - - - - - - - - - - 
    % - - CONVERT TO GRAYSCALE  - - -
    % - - - - - - - - - - - - - - - -
    for p = 0 : splicedImages-1;
        img = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', roundNmbr, p), 'bmp');
        % - - - - - - - - - - - - - - - - 
        % - - -  GrayScale Bitmap - - - -
        % - - - - - - - - - - - - - - - -
        grayImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
        grayImg = double(grayImg);
        grayImg = grayImg / max(grayImg(:));
        imgNmbrOfPixels = length(grayImg(:));
        p 
        imwrite(grayImg,sprintf('Round%d/splicedImages/splicedImage%d.bmp', roundNmbr, p));
    end
end
