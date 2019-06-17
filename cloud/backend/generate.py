import numpy as np

fs = 300
f = 10

x = np.arange(fs)
noise = np.cos(np.pi*f*x/fs)
y = np.sin(2*np.pi*f*x/fs) + 0.2*noise

with open("data.txt", "w") as f:
    res = ','.join(format(i,".2f") for i in y)
    f.write(res)
