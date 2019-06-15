import socket
import time
import pymongo
import struct
from struct import unpack
from datetime import datetime

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

  
if __name__ == '__main__':

# init socket
    server = SocketServer('', 8888)
    server.init_socket()
    server.listen(1)

# DB setup
    connection = pymongo.MongoClient("mongodb://localhost:27017")

    database = connection['my_database']
    collection_ABS = database['my_collection_ABS']
    collection_MAC = database['my_collection_MAC']

    list_MAC = ['MAC_X', 'MAC_Y']
    list_ABS = ['ABS_X', 'ABS_Y']

    n_bytes = 12 * 4

    while True:

        iResult = server.receive(n_bytes)
        
        timestamp = datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        
        if(iResult > 0):
            document_MAC = collection_MAC.insert_one({'timestamp' : timestamp, list_MAC[0]: struct.unpack('l', server.recv_buffer[0:4])[0],
                                                                           list_MAC[1]: struct.unpack('l', server.recv_buffer[4:8])[0]})
            document_ABS = collection_ABS.insert_one({'timestamp' : timestamp, list_ABS[0]: struct.unpack('l', server.recv_buffer[24:28])[0],
                                                                       list_ABS[1]: struct.unpack('l', server.recv_buffer[28:32])[0]})

        else:
            server.close_client()
            server.listen(1)

    


    