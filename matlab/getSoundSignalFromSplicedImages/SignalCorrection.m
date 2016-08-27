% - - - - - - - - - - - - - - - - 
% - - - Signal Correction - - - -
% - - - - - - - - - - - - - - - -

correction = a(1);
b = a;
for i = 1: length(a);
    b(i) = a(i) - correction;
    if b(i) > 0;
        correction = correction + 0.02;
    else
        correction = correction - 0.02;
    end
end