for p = 0 : 204;
    tmp1 = imread(sprintf('img/Round1/AIA%d.bmp',p*50 +1), 'bmp');

    % crop the black pixels!
    tmp1(end,:,:) = [];
    tmp1(:,end,:) = [];
    tmp1(1,:,:) = [];
    tmp1(:,1,:) = [];

    tmp1 = flipud(tmp1);
    tmp1 = rot90(tmp1);
    tmp1 = flipud(tmp1);

    for i = p*50 +2 : (p+1)*50;
        tmp = imread(sprintf('img/Round1/AIA%d.bmp',i));
        % crop the black pixels!
        tmp(end,:,:) = [];
        tmp(:,end,:) = [];
        tmp(1,:,:) = [];
        tmp(:,1,:) = [];

        tmp = flipud(tmp);
        tmp = rot90(tmp);
        tmp = flipud(tmp);

        tmp1 = cat(2,tmp1,tmp);
        i
    end

    imwrite(tmp1,sprintf('img/Round1/splicedImage%d.bmp',p));
end