import socket
import pymongo
import struct
from datetime import datetime
import matplotlib
import matplotlib.pyplot as plt
import numpy as np

BUFFERLIMIT = 60
PAYLOAD_MAX = 100
PAYLOAD_MIN = -100
SIZE_INT = 4
NUM_AXIS = 6


class SocketServer:
    def __init__(self, host, port):
        self.port = port
        self.host = host
        self.server_socket = None
        self.client_socket = None
        self.recv_buffer = "" 


    def init_socket(self):
        self.server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        print("Server socket created")

        # look closely. The bind() function takes tuple as argument
        self.server_socket.bind((self.host, self.port))
        print("Server socket bound with host {} port {}".format(self.host, self.port))

    
    def listen(self, num):
        print('Listening...')
        self.server_socket.listen(num)
        self.client_socket, address = self.server_socket.accept()  # accept new connection    
        print("Connection from: " + str(address))

    def receive(self, n_bytes):
        self.recv_buffer = self.client_socket.recv(n_bytes)
        print("{0} bytes data received ".format(len(self.recv_buffer)))
        return len(self.recv_buffer)
    
    def close_client(self):
        self.client_socket.close()

def Update_figure(idx, new_data):
    axes.set_xlim(0,idx)
    plot_data_x.append(idx)
    plot_data_y.append(new_data)
    line.set_xdata(plot_data_x)
    line.set_ydata(plot_data_y)
    plt.draw()
    plt.pause(1e-17)

if __name__ == '__main__':

# init socket
    server = SocketServer('', 8888)
    server.init_socket()
    server.listen(1)

# init plot    
    plot_data_x = []
    plot_data_y = []
    
    plt.show()
    plt.ylabel('PayLoad(%)')
    plt.title('Payload in last 1 minute.')
    plt.grid()
    axes = plt.gca()
    axes.set_ylim(PAYLOAD_MIN, PAYLOAD_MAX)
    axes.xaxis.set_ticklabels([])
    line, = axes.plot(plot_data_x, plot_data_y, 'ro-')

# DB setup
    connection = pymongo.MongoClient("mongodb://localhost:27017")

    database = connection['my_database']
    collection_ABS = database['my_collection_ABS']
    collection_MAC = database['my_collection_MAC']
    collection_LOAD = database['my_collection_LOAD']
    
    list_LOAD = ['LOAD_X', 'LOAD_Y']
    list_MAC = ['MAC_X', 'MAC_Y']
    list_ABS = ['ABS_X', 'ABS_Y']

    n_bytes = 3* NUM_AXIS * SIZE_INT
    
    idx = 0
    
    while True:

        iResult = server.receive(n_bytes)
        
        timestamp = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

            
        if(iResult > 0):
            
            X_MAC = struct.unpack('l', server.recv_buffer[0:4])[0]
            Y_MAC = struct.unpack('l', server.recv_buffer[4:8])[0]
            X_ABS = struct.unpack('l', server.recv_buffer[24:28])[0]
            Y_ABS = struct.unpack('l', server.recv_buffer[28:32])[0]
            X_Load = struct.unpack('l', server.recv_buffer[48:52])[0] + 
                np.random.random()*PAYLOAD_MAX*2 - PAYLOAD_MAX
            Y_Load = struct.unpack('l', server.recv_buffer[52:56])[0]
            
            document_MAC = collection_MAC.insert_one({'timestamp' : timestamp, list_MAC[0]: X_MAC,
                                                                           list_MAC[1]: Y_MAC})
            document_ABS = collection_ABS.insert_one({'timestamp' : timestamp, list_ABS[0]: X_ABS,
                                                                       list_ABS[1]: Y_ABS})           
            document_LOAD = collection_LOAD.insert_one({'timestamp' : timestamp, list_LOAD[0]: X_Load,
                                                                       list_LOAD[1]: Y_Load})            
            

            Update_figure(idx, X_Load)
            idx += 1
            if(idx == BUFFERLIMIT):
                plot_data_x = []
                plot_data_y = []
                idx = 0
                    
        else:
            server.close_client()
            server.listen(1)



    


    