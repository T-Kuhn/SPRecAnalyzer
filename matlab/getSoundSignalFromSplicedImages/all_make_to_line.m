%%% Script M-file:all_make_to_line.m %%%

%%%%%�y�\�\�\�א����������s���v���O�����\�\�\�z%%%%%
clear; close all;
r=input('�����ڂ̉摜�� = ');
S=input(' �n�߂̉摜�ԍ� = ');
E=input(' �I��̉摜�ԍ� = ');

load(sprintf('r%d_onkousuu',r));

S3=S;
a1=60; % �������ߎ��̒l�i�Ȃ�ׂ���������́j %
b1=a1;

for onkou=1:track
    S2=S;
    while S2<=E
        load(sprintf('r%d_UNIT%d-%d.mat',r,S2,onkou));
        UNIT1=UNIT;
        [mx,my]=size(UNIT1);       
    
%%%%%�y�\�\�\�א��������\�\�\�z%%%%%
        K1=rot90(UNIT1);
        K1=fliplr(K1);
        [ox,oy]=size(K1);
        H1=1:oy;
        
        for w1=1:ox
            qx=ox+1-w1;
            Y1=K1(qx,:);
            shiro1=sum(Y1);
            if shiro1<12
                shiro1=shiro3;
                Y1=Y3;
            end
            LINE1(w1)=sum(Y1.*H1)/shiro1;
            Y3=Y1;
            shiro3=shiro1;
            LINE4(w1)=LINE1(w1);
            if w1>=a1   % �������ߎ� %
                w1;
                XPOL1=w1:-1:w1-(a1-1);
                YPOL1=LINE1(w1:-1:w1-(a1-1));
                pol1=polyfit(XPOL1,YPOL1,2);
                YPOLY1=polyval(pol1,XPOL1);
                LINE4(XPOL1(a1/2))=YPOLY1(a1/2);
            end
        end
        save(sprintf('r%d_LINE%d-%d',r,S2,onkou),'LINE4'); 

        S2=S2+1;
    end
end


