% - - - - - - - - - - - - - - - - 
% - - -  Splice Bot Class - - - -
% - - - - - - - - - - - - - - - -
classdef SpliceBot < handle
    properties
    CurrentRoundNmbr;
    NmbrOfImages;
    MaxFolderNmbr;
    NmbrOfSplicedImages;
    end
    methods
        % - - - - - - - - - - - - - - - - 
        % - - - - - Constructor - - - - -
        % - - - - - - - - - - - - - - - -
        function obj = SpliceBot()
            obj.CurrentRoundNmbr = 1;
            obj.GetNmbrOfFolders();
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Start - - - - - - -
        % - - - - - - - - - - - - - - - -
        function Start(obj)
            while obj.CurrentRoundNmbr <= obj.MaxFolderNmbr; 
                obj.GetNmbrOfFiles();
                obj.AddImages();
                obj.AddRemainingImages();
                obj.GetNmbrOfSplicedImages();
                obj.ConvertToGrayScale();
                obj.Next();
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - - Next  - - - - - - -
        % - - - - - - - - - - - - - - - -
        function Next(obj)
        obj.CurrentRoundNmbr = obj.CurrentRoundNmbr + 1;
        end
        % - - - - - - - - - - - - - - - - 
        % - - - - - Add Images  - - - - -
        % - - - - - - - - - - - - - - - -
        function AddImages(obj)
            
            splicedFullImages = floor(obj.NmbrOfImages/50);

            for p = 0 : splicedFullImages-1;
                tmp1 = imread(sprintf('Round%d/rawdata/AIA%d.bmp',obj.CurrentRoundNmbr,p*50 +1), 'bmp');

                % crop the black pixels!
                tmp1(end,:,:) = [];
                tmp1(:,end,:) = [];
                tmp1(1,:,:) = [];
                tmp1(:,1,:) = [];

                tmp1 = flipud(tmp1);
                tmp1 = rot90(tmp1);
                tmp1 = flipud(tmp1);

                for i = p*50 +2 : (p+1)*50;
                    tmp = imread(sprintf('Round%d/rawdata/AIA%d.bmp',obj.CurrentRoundNmbr,i));
                    % crop the black pixels!
                    tmp(end,:,:) = [];
                    tmp(:,end,:) = [];
                    tmp(1,:,:) = [];
                    tmp(:,1,:) = [];

                    tmp = flipud(tmp);
                    tmp = rot90(tmp);
                    tmp = flipud(tmp);

                    tmp1 = cat(2,tmp1,tmp);
                end
                disp(p+1);
                imwrite(tmp1,sprintf('Round%d/splicedImages/splicedImage%d.bmp',obj.CurrentRoundNmbr,p+1));
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - AddRemainingImages  - - -
        % - - - - - - - - - - - - - - - -
        function AddRemainingImages(obj)
            splicedFullImages = floor(obj.NmbrOfImages/50);
            rest = mod(obj.NmbrOfImages, 50);
            if rest == 0
                return;
            end

            tmp1 = imread(sprintf('Round%d/rawdata/AIA%d.bmp',obj.CurrentRoundNmbr,splicedFullImages*50 +1), 'bmp');

            % crop the black pixels!
            tmp1(end,:,:) = [];
            tmp1(:,end,:) = [];
            tmp1(1,:,:) = [];
            tmp1(:,1,:) = [];

            tmp1 = flipud(tmp1);
            tmp1 = rot90(tmp1);
            tmp1 = flipud(tmp1);
                
            %for i = 10252 : 10285;
            for i = splicedFullImages*50 +2 : obj.NmbrOfImages;
                tmp = imread(sprintf('Round%d/rawdata/AIA%d.bmp',obj.CurrentRoundNmbr,i));
                % crop the black pixels!
                tmp(end,:,:) = [];
                tmp(:,end,:) = [];
                tmp(1,:,:) = [];
                tmp(:,1,:) = [];
                
                tmp = flipud(tmp);
                tmp = rot90(tmp);
                tmp = flipud(tmp);
                
                tmp1 = cat(2,tmp1,tmp);
                
            end
            disp(splicedFullImages+1);
            imwrite(tmp1,sprintf('Round%d/splicedImages/splicedImage%d.bmp',obj.CurrentRoundNmbr, splicedFullImages + 1));
        end
        % - - - - - - - - - - - - - - - - 
        % - - CONVERT TO GRAYSCALE  - - -
        % - - - - - - - - - - - - - - - -
        function ConvertToGrayScale(obj)
            for p = 1 : obj.NmbrOfSplicedImages;
                img = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p), 'bmp');
                
                grayImg = img(:,:,1) + img(:,:,2) + img(:,:,3);
                grayImg = double(grayImg);
                grayImg = grayImg / max(grayImg(:));
                imgNmbrOfPixels = length(grayImg(:));
                p 
                imwrite(grayImg,sprintf('Round%d/splicedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p));
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - - CONVERT TO BW IMAGES  - -
        % - - - - - - - - - - - - - - - -
        function ConvertToBWImages(obj)
            for p = 1 : obj.NmbrOfSplicedImages;
                img = imread(sprintf('Round%d/splicedImages/splicedImage%d.bmp', obj.CurrentRoundNmbr, p), 'bmp');
                bwImg = false(size(img));
                bwImg(img > 250) = true;
                p 
                imwrite(bwImg,sprintf('Round%d/BWImages/BWImage%d.bmp', obj.CurrentRoundNmbr, p));
            end
        end
        % - - - - - - - - - - - - - - - - 
        % - - Get Number Of folders - - -
        % - - - - - - - - - - - - - - - -
        function GetNmbrOfFolders(obj)
            D = dir(['.', '\Round*']);
            obj.MaxFolderNmbr = length(D([D.isdir]));
        end
        % - - - - - - - - - - - - - - - - 
        % - - - Get Number Of files - - -
        % - - - - - - - - - - - - - - - -
        function GetNmbrOfFiles (obj)
            D = dir([sprintf('Round%d/rawdata', obj.CurrentRoundNmbr), '\*.bmp']);
            obj.NmbrOfImages = length(D(not([D.isdir])))
        end
        % - - - - - - - - - - - - - - - - 
        % -  Get Number Of spliced Imgs -
        % - - - - - - - - - - - - - - - -
        function GetNmbrOfSplicedImages (obj)
            D = dir([sprintf('Round%d/splicedImages', obj.CurrentRoundNmbr), '\*.bmp']);
            obj.NmbrOfSplicedImages = length(D(not([D.isdir])))
        end
    end
end
