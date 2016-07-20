% use the "TwoValImg" matrix as data
X = TwoValImg;
[m,n] = size(X);

X(1,:) = 0;
X(m,:) = 0;
X(:,1) = 0;
X(:,n) = 0;

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
% colormap(gray);

% delete overlapping labels
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
figure(5);
imagesc(X); axis image;


