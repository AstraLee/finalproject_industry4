import socket
import time
import pymongo
import struct
from struct import unpack
from datetime import datetime

## server connection
def server_program():
    host = ''
    port = 8888  

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    print("Server socket created")

    # look closely. The bind() function takes tuple as argument
    server_socket.bind((host, port))
    print("Server socket bound with host {} port {}".format(host, port))
    print('Listening...')

    # configure how many client the server can listen simultaneously
    server_socket.listen(1)
    conn, address = server_socket.accept()  # accept new connection
    
    print("Connection from: " + str(address))

    connection = pymongo.MongoClient("mongodb://localhost:27017")

    database = connection['my_database']
    collection_ABS = database['my_collection_ABS']
    collection_MAC = database['my_collection_MAC']

    list_MAC = ['MAC_X', 'MAC_Y', 'MAC_Z', 'MAC_A', 'MAC_B', 'MAC_C']
    list_ABS = ['ABS_X', 'ABS_Y', 'ABS_Z', 'ABS_A', 'ABS_B', 'ABS_C']
    
    while True:
        data = conn.recv(12*4)

        for i in range(len(list_MAC)-1):
            document_MAC = collection_MAC.insert_one({'timestamp' : datetime.now().strftime('%Y-%m-%d %H:%M:%S'), list_MAC[i]: struct.unpack('l', data[4*i:4*(i+1)])[0]})
        
        for i in range(len(list_ABS)-1):
            document_ABS = collection_ABS.insert_one({'timestamp' : datetime.now().strftime('%Y-%m-%d %H:%M:%S'), list_ABS[i]: struct.unpack('l', data[4*(i+6):4*(i+7)])[0]})

    conn.close()  # close the connection
  
if __name__ == '__main__':
    server_program()
    

    


    