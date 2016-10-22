function val = makeBigImage(x, y)
    roundNmbr = x;
    splicedImages = y; 
    bigImage = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', roundNmbr, 1), 'bmp');
    
    % - - - - - - - - - - - - - - - - 
    % - - CONVERT TO GRAYSCALE  - - -
    % - - - - - - - - - - - - - - - -
    for p = 2 : splicedImages;
        img = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', roundNmbr, p), 'bmp');
        % - - - - - - - - - - - - - - - - 
        % - - -  GrayScale Bitmap - - - -
        % - - - - - - - - - - - - - - - -
        bigImage = cat(2,bigImage,img);
        
    end
    imwrite(bigImage,sprintf('BigImage.bmp'));
    
    %save a resized version of the bigImage
    res = [];
    for p = 0:round(166140/30) - 1;
    val = sum(rot90(bigImage(:, (30*p+1):30*p + 30)));
    val = round(val/30);
    res = cat(1,res,val);
    end
    imwrite(uint8(res),sprintf('resizedBigImage.bmp'));
end
