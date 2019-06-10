#ifndef _SOCKETSERVER_
#define _SOCKETSERVER_

#include <WinSock2.h>
#include <iostream>



#pragma comment(lib, "ws2_32.lib") // load static lib

class SocketServer{

public:
		SocketServer(const char* IP_addr, UINT port,  bool UDP = false);
		SocketServer(const char* IP_addr, UINT port,  UINT recvBufferLen, UINT sendBufferLen, bool UDP = false);

		~SocketServer();



public:

		void Set_BufferLen(UINT recvLen, UINT sendLen);
		void Get_recvBuffer(void* Buffer, UINT count) ;
		void Set_sendBuffer(char* sendBuffer);
		void closeSocket();
		void _receiveMessage();
		void acceptAndBlock();


private:

		SOCKET clientSocket;
		SOCKET serverSocket;
		int _port;
		int _recvBufferLen;
		int _sendBufferLen;
		int _iResult;
		char* _recvBuffer;
		char* _sendBuffer;

		void _sendMessage(char* sendBuffer);		

};

#endif