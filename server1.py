import socket
import time
import pymongo
import struct
from struct import unpack


## server connection
def server_program():
    # get the hostname
    host = ''
    port = 8888  

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)  # get instance
    print("Server socket created")

    # look closely. The bind() function takes tuple as argument
    server_socket.bind((host, port))  # bind host address and port together
    print("Server socket bound with host {} port {}".format(host, port))
    print('Listening...')
    # configure how many client the server can listen simultaneously
    server_socket.listen(1) #for eg. 5
    conn, address = server_socket.accept()  # accept new connection
    print("Connection from: " + str(address))

    connection = pymongo.MongoClient("mongodb://localhost:27017")

    database = connection['my_database']
    collection = database['my_collection']

    list_MAC = ['MAC_X', 'MAC_Y', 'MAC_Z', 'MAC_A', 'MAC_B', 'MAC_C']
    list_ABS = ['ABS_X', 'ABS_Y', 'ABS_Z', 'ABS_A', 'ABS_B', 'ABS_C']
    
    while True:
        data = conn.recv(12*4)

        for i in range(len(list_MAC)-1):
            document_MAC = collection.insert_one({list_MAC[i]: struct.unpack('l', data[4*i:4*(i+1)])})
            document_ABS = collection.insert_one({list_ABS[i]: struct.unpack('l', data[4*(i+6):4*(i+7)])})
            print(database.collection.document_MAC.find())

    conn.close()  # close the connection
  
if __name__ == '__main__':
    server_program()

    # dataset (ITERATIVELY)
    

    


    