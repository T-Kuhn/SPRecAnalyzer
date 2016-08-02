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
PixelObjects = PixelObject_.empty(0, 0);
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
                PixelObjects(map(tmp)).addPoint([kx, ky]);
            else
                % The thing didn't exist!
                indexCounter = indexCounter + 1;
                existingPixelObjects(indexCounter) = tmp;            
                % Here we need to add another entry to the map!
                map(tmp) = indexCounter;
                PixelObjects(indexCounter) = PixelObject_(indexCounter);
                PixelObjects(indexCounter).addPoint([kx, ky]);
            end
        end
    end
end

% - - - - - - - - - - - - - - - - 
%  PRINT NMBR OF BIG PIXELOBJTS -
% - - - - - - - - - - - - - - - -

pixelThreshold = 1000;
nmbrOfObjects = 0;

for ent = PixelObjects;
    if ent.EntryIndex > pixelThreshold;
        ent.EntryIndex
        nmbrOfObjects = nmbrOfObjects + 1;
    end
end

nmbrOfObjects

% one of the next things would be to calculate the centerPositions of all those things
% with more than pixelThreshold pixels!

% Then, last but not least, we could return the center position of the thing
% most to the left!

% after getting that data to the good old c# thing we could try to make the position calculations.
% that shouldn't be that hard actually.






