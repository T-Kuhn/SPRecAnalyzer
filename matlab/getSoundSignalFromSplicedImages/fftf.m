function [X, f, y, y2] = fftf(t, x, varargin)
% 	 - fft filter;
%   [X, f, y, y2] = fftf(t, x); with t the time vector and x the signal,
%   displays the original signal, the Fourier transform (absolute values)
%   and the reconstructed signal generated by the inverse transform ifft
%   with a selected subset of the frequencies.
%   By default, the frequencies in the filtered signal are cut at 1/8 the
%   sampling frequency.
%   The function returns X - reconstructed signal, f - vector of frequencies, y -
%   full vector of amplitudes, y2 - the filtered vector of amplitudes.
%   [X, f, y, y2] = fftf(t, x, cutoff); the user may set the cutoff
%   frequency in units of Hertz.
%   [X, f, y, y2] = fftf(t, x, cutoff, my_N); the user may select my_N
%   amplitudes with highest abolute value to participate in the
%   reconstruction.
%   Examples
%   (suppose you have t and x defined already, both 1-dimensional vectors of the same length.)
%   fftf(t, x, 1e6); - reconstruct the signal with frequencies lower or
%   equal to cutoff value of 1MHz.
%   fftf(t, x, [], 20); - use only 20 biggest amplitudes for
%   reconstruction.
%   fftf(t, x, 1e6, 20); - select 20 biggest amplitudes within cutoff.
% Shmuel Ben-Ezra, Ultrashape ltd. August 2009

%% Verifying input
if ~any(size(t)==1),
   disp('Unexpected vector size! - should be 1D vectors.')
   return
end
if ~any(size(x)==1),
   disp('Unexpected vector size! - should be 1D vectors.')
   return
end
if length(t)~=length(x),
   disp('Unexpected vector size! - should be same length.')
   return
end
%% Definitions
Fs=1/(t(2)-t(1)); %sampling freq
N=length(x);
Nfft=2^nextpow2(N);
f=Fs/2*linspace(0,1,1+Nfft/2); % create freqs vector
cutoff_freq=Fs/8;
cutoff_freqLow = 0;
my_freqs=[];
if nargin>2,
    cutoff_freq=varargin{1};
end
if nargin>3,
    cutoff_freqLow=varargin{2};
end
if nargin>4,
    my_freqs=varargin{3};
end
%% main
y=fft(x,Nfft)/N; % perform fft transform
y2=filterfft(f, y, cutoff_freq, my_freqs, cutoff_freqLow); % filter amplitudes
%X=ifft(y2,'symmetric'); % the inverse transform. 'symmetric' is not recognized in older versions of matlab 
X=ifft(y2); % inverse transform
X=X(1:N)/max(X);
ind1 = find(y2(1:1+Nfft/2)); % get the nonzero elements in y2
nf1 = length(ind1); % count nonzero elements
%% display
figname = 'fftf - FFT at work';
ifig = findobj('type', 'figure', 'name', figname);
if isempty(ifig),
    ifig = figure('name', figname); % on my machine: ..., 'position', [360   120   600   800]);
end
figure(ifig);
% first plot
subplot(3,1,1)
plot(t*1e6,x)
xlabel('uSec')
axis tight
title('Original signal')
%second plot
subplot(3,1,2)
yplot=abs(y(1:1+Nfft/2));
yplot=yplot/max(yplot);
semilogy(f*1e-6, yplot, f(ind1)*1e-6, yplot(ind1), '.r');
xlabel('MHz')
title('Amplitudes')
legend('full spectrum', 'selected frequencies')
% third plot
subplot(3,1,3)
plot(t*1e6,X)
xlabel('uSec')
if isempty(cutoff_freq),
    scutoff='No cutoff.';
else
    scutoff=sprintf('Cutoff = %g [Mhz]', cutoff_freq/1e6);
end
stitle3=sprintf('Reconstructed signal with %d selected frequencies; %s', nf1, scutoff);
title(stitle3)
axis tight
return

function y2=filterfft(f, y, cutoff, wins, cutoffLow)
nf=length(f);
ny=length(y);
if ~(ny/2+1 == nf),
    disp('unexpected dimensions of input vectors!')
    y2=-1;
    return
end

% cutoff filter
y2=zeros(1,ny);
if ~isempty(cutoff) && (cutoffLow > 0);
    display('we are in here');
    ind1=find(f<=cutoff & f>=cutoffLow);
    %display(ind1);
    y2(ind1) = y(ind1); % insert required elements
else
    y2=y;
end

% dominant freqs filter
if ~isempty(wins),
    temp=abs(y2(1:nf));
    y2=zeros(1,ny);
    for k=1:wins,   % number of freqs that I want
        [tmax, tmaxi]=max(temp);
        y2(tmaxi) = y(tmaxi); % insert required element
        temp(tmaxi)=0; % eliminate candidate from list
    end
end

% create a conjugate symmetric vector of amplitudes
for k=nf+1:ny,
    y2(k) = conj(y2(mod(ny-k+1,ny)+1)); % formula from the help of ifft
end
return