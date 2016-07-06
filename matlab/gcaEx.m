% get current axes (gca) example
clear; close all;

x = 0:0.05:25.0;
y0 = besselj(0,x);
y1 = besselj(1,x);
y2 = besselj(2,x);
y3 = besselj(3,x);
y = y0*0;

% Figure
plot(x,y0,x,y1,x,y2,x,y3,x,y,'LineWidth',2.0)
grid on
xlabel('Radian','FontSize',12)
ylabel('Value of Bessel function', 'FontSize',12)
gtext('J0'); gtext('J1'); gtext('J2'); gtext('J3');
axis([0.0 20.0 -0.5 1.0]);
xt = 0:2.5:20; yt = 0.5:0.25:1.0;
ax = gca;
set(ax,'FontSize',12,'XTick', xt, 'YTick', yt)
% EOF