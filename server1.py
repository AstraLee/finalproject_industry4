import socket
import time
import pymongo
from bson import ObjectId
import pprint

def main():
    # Connect to MongoDB instance
    connection = pymongo.MongoClient("mongodb://localhost:27017")

    database = connection['my_database']
    collection = database['my_collection']

    encoder = database.encoder_values
    print('Total Record for the collection: ' + str(encoder.count()))

    results = encoder.find()
  

def insert_data(data):
    """
    Insert new data or document in collection
    :param data:
    :return:
    """
    document = collection.insert_one(data)
    return document.inserted_id


def update_or_create(document_id, data):
    """
    This will create new document in collection
    IF same document ID exist then update the data
    :param document_id:
    :param data:
    :return:
    """
    # TO AVOID DUPLICATES - THIS WILL CREATE NEW DOCUMENT IF SAME ID NOT EXIST
    document = collection.update_one({'_id': ObjectId(document_id)}, {"$set": data}, upsert=True)
    return document.acknowledged


def get_single_data(document_id):
    """
    get document data by document ID
    :param document_id:
    :return:
    """
    data = collection.find_one({'_id': ObjectId(document_id)})
    return data


def get_multiple_data():
    """
    get document data by document ID
    :return:
    """
    data = collection.find()
    return list(data)


def update_existing(document_id, data):
    """
    Update existing document data by document ID
    :param document_id:
    :param data:
    :return:
    """
    document = collection.update_one({'_id': ObjectId(document_id)}, {"$set": data})
    return document.acknowledged


def remove_data(document_id):
    document = collection.delete_one({'_id': ObjectId(document_id)})
    return document.acknowledged

def server_program():
    # get the hostname
    host = ''
    port = 5001  # initiate port no above 1024

    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)  # get instance
    print("Server socket created")

    # look closely. The bind() function takes tuple as argument
    server_socket.bind((host, port))  # bind host address and port together
    print("Server socket bound with host {} port {}".format(host, port))
    print('Listening...')
    # configure how many client the server can listen simultaneously
    server_socket.listen(5) #for eg. 5
    conn, address = server_socket.accept()  # accept new connection
    
    count = 0
    
    print("Connection from: " + str(address))
    while True:
        count = count + 1
        print("Accepted {} connections so far".format(count))

        # receive data stream. it won't accept data packet greater than 1024 bytes
        data = conn.recv(1024).decode()
        currentTime = time.ctime(time.time()) + "\r\n"
        conn.send(currentTime.encode('ascii'))

        if not data:
            # if data is not received break
            break
        print("from connected user: \'" + str(data) + '\' ' + str(currentTime))
        data = input(' -> ')
        conn.send(data.encode())  # send data to the client
    
    conn.close()  # close the connection


if __name__ == '__main__':
    server_program()
    #main()

    ## dataset (ITERATIVELY)
    

    


    