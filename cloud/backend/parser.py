#!/usr/bin/env python3
import json
import asyncio
import websockets

with open("data.txt", "r") as f:
    data = f.read()
d = { "label": "x",
      "data": data }
payload = json.dumps(d)


async def sendMessage(websocket, path):
    await websocket.send(payload)
    
def main():
    start_server = websockets.serve(sendMessage, '127.0.0.1', 8081)
    asyncio.get_event_loop().run_until_complete(start_server)
    asyncio.get_event_loop().run_forever()
    

if __name__ == "__main__":
    main()
