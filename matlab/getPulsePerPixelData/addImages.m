tmp1 = imread('img/AIA1000.bmp', 'bmp');

% crop the black pixels!
tmp1(end,:,:) = [];
tmp1(:,end,:) = [];
tmp1(1,:,:) = [];
tmp1(:,1,:) = [];

for i = 999 : -1 : 1;
    tmp = imread(sprintf('img/AIA%d.bmp',i));
    % crop the black pixels!
    tmp(end,:,:) = [];
    tmp(:,end,:) = [];
    tmp(1,:,:) = [];
    tmp(:,1,:) = [];
    
    tmp1 = cat(1,tmp1,tmp);
    i
end

imwrite(tmp1,'tmp1new.bmp');