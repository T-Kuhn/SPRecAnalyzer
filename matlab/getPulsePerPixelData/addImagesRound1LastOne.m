tmp1 = imread('img/Round1/AIA10251.bmp', 'bmp');

% crop the black pixels!
tmp1(end,:,:) = [];
tmp1(:,end,:) = [];
tmp1(1,:,:) = [];
tmp1(:,1,:) = [];

tmp1 = flipud(tmp1);
tmp1 = rot90(tmp1);
tmp1 = flipud(tmp1);
    
for i = 10252 : 10285;
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

imwrite(tmp1,'splicedImage206.bmp');