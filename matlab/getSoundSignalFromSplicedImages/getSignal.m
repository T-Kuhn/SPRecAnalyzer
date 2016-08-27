% - - - - - - - - - - - - - - - - 
% - - - - - GET SIGNAL  - - - - -
% - - - - - - - - - - - - - - - -

% initialize bot
bot = SigExBot();

for p = 0 : 205;
    % - - - - - - - - - - - - - - - - 
    % - - - Load Grayscale Image  - -
    % - - - - - - - - - - - - - - - -
    img = imread(sprintf('splicedImages/splicedImage%d.bmp',p), 'bmp');




    imwrite(Img,sprintf('botImages/botImage%d.bmp',p));
end
