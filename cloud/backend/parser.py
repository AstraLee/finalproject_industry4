#!/usr/bin/env python3
import json
import asyncio
import socket
import websockets

with open("data.txt", "r") as f:
    data = f.read()
d = { "label": "x",
      "data": data }
payload = json.dumps(d)


async def sendMessage(websocket, path):
    await websocket.send(payload)
    
def main():
    start_server = websockets.serve(sendMessage, '140.112.14.145', 8081)
    asyncio.get_event_loop().run_until_complete(start_server)
    asyncio.get_event_loop().run_forever()
    

if __name__ == "__main__":
    main()


"""
async def sendMessage(websocket, path):
    await websocket.send(payload)
    
def main():
    start_server = websockets.serve(sendMessage, '140.112.14.145', 8081)
    asyncio.get_event_loop().run_until_complete(start_server)
    asyncio.get_event_loop().run_forever()


if __name__ == "__main__":
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect(('127.0.0.1', 8888))
        data = s.recv(1024)
        print('Received', repr(data))
        payload = json.dumps(data)

    main()

"""