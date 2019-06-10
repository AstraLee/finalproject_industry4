#include "stdafx.h"
#include <WinSock2.h>
#include <iostream>
#include "SocketServer.h"

#pragma comment(lib, "ws2_32.lib")

 #define _DEBUG_

SocketServer::SocketServer(const char* IP_addr ,UINT port, bool UDP)
: _port(port),
_recvBufferLen(1000),
_sendBufferLen(1000)
{
    // load static lib
	WSADATA wsa;
    WSAStartup(MAKEWORD(2,0), &wsa);
	if (!UDP){
		if ((serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)) <= 0){
			printf("Createing serverSocket failed! \n");
			return;
		}
    }
	else{
		if ((serverSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)) <= 0){
			printf("Createing serverSocket failed! \n");
			return;
		}
	}
	SOCKADDR_IN serveraddr;
	serveraddr.sin_family = AF_INET;   // ipv4
	serveraddr.sin_port = htons(_port);
	serveraddr.sin_addr.S_un.S_addr = inet_addr(IP_addr);
	if (bind(serverSocket, (SOCKADDR *)&serveraddr, sizeof(serveraddr)) != 0){
		printf("Binding socket failed! \n ");
		return;

	}

	printf("Start listening...\n");

	if (listen(serverSocket, 1) != 0){
		printf("listening failed!\n");
		return;
	}
	_recvBuffer = new char[_recvBufferLen];
	_sendBuffer = new char[_sendBufferLen];
	memset(_recvBuffer, 0, sizeof(_recvBuffer));
	memset(_sendBuffer, 0, sizeof(_sendBuffer));

}

SocketServer::SocketServer(const char* IP_addr, UINT port,  UINT recvLen, UINT sendLen, bool UDP)
: _port(port),
_recvBufferLen(recvLen),
_sendBufferLen(sendLen)
{
	// load static lib
	WSADATA wsa;
	WSAStartup(MAKEWORD(2, 0), &wsa);
	if (!UDP){
		if ((serverSocket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)) <= 0){
			printf("Createing serverSocket failed! \n");
			return;
		}
	}
	else{
		if ((serverSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)) <= 0){
			printf("Createing serverSocket failed! \n");
			return;
		}
	}
	SOCKADDR_IN serveraddr;
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(_port);
	serveraddr.sin_addr.S_un.S_addr = inet_addr(IP_addr);
	if (bind(serverSocket, (SOCKADDR *)&serveraddr, sizeof(serveraddr)) != 0){
		printf("Binding socket failed! \n ");
		return;
	}
	printf("Start listening...\n");

	if (listen(serverSocket, 1) != 0){
		printf("listening failed!\n");
		return;
	}
	_recvBuffer = new char[_recvBufferLen];
	_sendBuffer = new char[_sendBufferLen];
	memset(_recvBuffer, 0, sizeof(_recvBuffer));
	memset(_sendBuffer, 0, sizeof(_sendBuffer));
}

SocketServer::~SocketServer(){
	closeSocket();
}

void SocketServer::closeSocket(){
	if (closesocket(serverSocket) == 0){
		printf("Socket closed!");
	}
	WSACleanup();
	if (_recvBufferLen != 0)
		delete[] _recvBuffer;
	if (_sendBufferLen != 0)
		delete[] _sendBuffer;
}

void SocketServer::acceptAndBlock(){
	int len = sizeof(SOCKADDR_IN);
	SOCKADDR_IN clientaddr;
	if ((clientSocket = accept(serverSocket, (SOCKADDR*)&clientaddr, &len)) <= 0){
		printf("connection acception failed!\n");
		return;
	}
}


void SocketServer::_receiveMessage(){
	//memset(_recvBuffer, '\0', _recvBufferLen);
	_iResult = recv(clientSocket, _recvBuffer, _recvBufferLen, 0);
	if (_iResult > 0){
#ifdef _DEBUG_
		printf("Bytes received: %d\n", _iResult);
#endif
	}
	else if (_iResult == 0)
		printf("Connection closing...\n");
	else{
		printf("recv failed with error: %d\n", WSAGetLastError());
		closesocket(clientSocket);
		WSACleanup();
	}
}

void SocketServer::_sendMessage(char* sendBuffer){
		
	_iResult = send(clientSocket, sendBuffer, sizeof(sendBuffer), 0);
		if (_iResult > 0){
#ifdef _DEBUG_
			printf("Bytes sent: %d\n", _iResult);
#endif
		}
		else if (_iResult == 0)
			printf("Connection closing...\n");
		else{
			printf("send failed with error: %d\n", WSAGetLastError());
			WSACleanup();
		}

}


void SocketServer::Get_recvBuffer(void* buffer, UINT count) {
	count = count > _recvBufferLen? _recvBufferLen:count;
	memcpy((void*)buffer, (void*)_recvBuffer, count);
}

void SocketServer::Set_BufferLen(UINT recvLen, UINT sendLen){

	delete[] _recvBuffer;
	delete[] _sendBuffer;
	_recvBufferLen = recvLen;
	_sendBufferLen = sendLen;
	_recvBuffer = new char[recvLen];
	_sendBuffer = new char[sendLen];
}		


void SocketServer::Set_sendBuffer(char* sendBuffer){
	_sendMessage(sendBuffer);
}