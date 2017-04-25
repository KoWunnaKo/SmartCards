//#include <stdio.h> /* Standard I/O */
#include <cstdio>
#include <cstdlib>
#include <String>
#include <dos.h>
#include <windows.h> /* Win32 API */
#include <winscard.h> /* SmartCard API */
#pragma once

using namespace System;

namespace SCfunctional {

	public ref class CardOperationApi
	{
	public:
		static BYTE ErrorCheck(char * string, LONG retval) {
			if (SCARD_S_SUCCESS != retval) {
				printf("Failed ");
				printf("%s: 0x%08X\n", string, retval);
				return 1;
			}
			return 0;
		}

		static void PrintResponse(BYTE * bReponse, DWORD dwLen) {
			printf("Response: ");
			for (int i = 0; i<dwLen; i++)
				printf("%02X ", bReponse[i]);
			printf("\n");
		}

		int get_RSA_components(DWORD *public_exponent, DWORD *public_modulus, DWORD *private_N, DWORD *private_D) {
			SCARDCONTEXT hSC = 0;
			SCARDHANDLE hCardHandle = 0;
			SCARD_IO_REQUEST pioSendPci;

			LPCTSTR pmszReaders = NULL;
			LONG lReturn = 0;
			DWORD cch = SCARD_AUTOALLOCATE;
			DWORD dwActiveProtocol;

			BYTE pbRecv[258];
			DWORD dwRecv;

			//selecting to rsa generating app
			BYTE APDU_Select_RSA_applet[] = { 0x00, 0xA4, 0x04, 0x00, 0x0B, 0x4A, 0x61, 0x76, 0x61,
				0x43, 0x61, 0x72, 0x64, 0x4F, 0x53, 0x03 };
			//get public exponent
			BYTE APDU_get_public_exponent[] = { 0x80, 0x31, 0x01, 0x00, 0x00 };
			//get public key_N
			BYTE APDU_get_public_key_N[] = { 0x80, 0x31, 0x00, 0x00, 0x00 };
			//public get private key N 
			BYTE APDU_get_private_key_N[] = { 0x00, 0x32, 0x00, 0x00, 0x00 };
			//public get private key D 
			BYTE APDU_get_private_key_D[] = { 0x00, 0x32, 0x01, 0x00, 0x00 };

			// Establish the context.
			lReturn = SCardEstablishContext(
				SCARD_SCOPE_USER,
				NULL,
				NULL,
				&hSC);
			if (ErrorCheck("SCardEstablishContext", lReturn))
				return -1;

			// Retrieve the list the readers.
			lReturn = SCardListReaders(hSC,
				NULL,
				(LPTSTR)&pmszReaders,
				&cch);

			switch (lReturn) {
			case SCARD_E_NO_READERS_AVAILABLE:
				printf("Reader is not in groups.\n");
				return -1;
				break;

			case SCARD_S_SUCCESS:
				// Display print reader.
				printf("Reader: %s\n", pmszReaders);
				break;

			default:
				printf("Failed SCardListReaders\n");
				return -2;
			}

			lReturn = SCardConnect(hSC,
				(LPCTSTR)pmszReaders,
				SCARD_SHARE_SHARED,
				SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1,
				&hCardHandle,
				&dwActiveProtocol);
			if (ErrorCheck("SCardConnect", lReturn))
				return -3;

			// Use the connection.
			// Display the active protocol and define protocol.
			switch (dwActiveProtocol) {
			case SCARD_PROTOCOL_T0:
				printf("Active protocol T0\n");
				pioSendPci = *SCARD_PCI_T0;
				break;

			case SCARD_PROTOCOL_T1:
				printf("Active protocol T1\n");
				pioSendPci = *SCARD_PCI_T1;
				break;

			case SCARD_PROTOCOL_UNDEFINED:
			default:
				printf("Active protocol unnegotiated or unknown\n");
				return -4;
			}

			// Transmit the request.
			// Select app.
			printf("Select RSA keys generating applet:\n");
			dwRecv = sizeof(pbRecv);
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_Select_RSA_applet,
				sizeof(APDU_Select_RSA_applet),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Applet doesn't exist on smartcard!");
				return -5;
			}

			PrintResponse(pbRecv, dwRecv);


			//Getting public exponent:
			printf("Getting public exponent:\n");
			dwRecv = sizeof(pbRecv);
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_get_public_exponent,
				sizeof(APDU_get_public_exponent),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("There is no RSA keys!");
				return -6;
			}
			PrintResponse(pbRecv, dwRecv);
			printf("Public exponent length: %02X \nData: ", dwRecv - 2);
			///  Public exponent!!!
			for (int i = 0; i < 3; i++) {
				public_exponent[i] = pbRecv[i];
				printf("%02X ", public_exponent[i]);
			}
			printf("\n");
			///


			//get public key N:
			printf("Getting public key N:\n");
			dwRecv = sizeof(pbRecv);
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_get_public_key_N,
				sizeof(APDU_get_public_key_N),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Error! Unable to get public key data!");
				return -7;
			}
			PrintResponse(pbRecv, dwRecv);
			printf("Public modulus length: %02X \nData: ", dwRecv - 2);
			///  Public modulus!!!
			for (int i = 0; i < dwRecv - 2; i++) {
				public_modulus[i] = pbRecv[i];
				printf("%02X ", public_modulus[i]);
			}
			printf("\n");

			//get private key N:
			printf("Getting private key N:\n");
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_get_private_key_N,
				sizeof(APDU_get_private_key_N),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Error! Unable to get private key data!");
				return -8;
			}
			for (int i = 0; i < dwRecv - 2; i++) {
				private_N[i] = pbRecv[i];
				printf("%02X ", private_N[i]);
			}
			printf("\n");

			printf("Getting private key N:\n");
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_get_private_key_D,
				sizeof(APDU_get_private_key_D),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Error! Unable to get private key data!");
				return -9;
			}
			for (int i = 0; i < dwRecv - 2; i++) {
				private_D[i] = pbRecv[i];
				printf("%02X ", private_D[i]);
			}
			printf("\n");
			///
			// Free the memory.
			lReturn = SCardFreeMemory(
				hSC,
				pmszReaders
			);
			if (ErrorCheck("SCardFreeMemory", lReturn))
				return -11;
			// Free the context.
			SCardReleaseContext(hSC);
			if (ErrorCheck("SCardReleaseContext", lReturn))
				return -12;

			return 0;
			return 0;
		}
		int generate_RSA() {
			SCARDCONTEXT hSC = 0;
			SCARDHANDLE hCardHandle = 0;
			SCARD_IO_REQUEST pioSendPci;

			LPCTSTR pmszReaders = NULL;
			LONG lReturn = 0;
			DWORD cch = SCARD_AUTOALLOCATE;
			DWORD dwActiveProtocol;

			BYTE pbRecv[258];
			DWORD dwRecv;

			//selecting to rsa generating app
			BYTE APDU_Select_RSA_applet[] = { 0x00, 0xA4, 0x04, 0x00, 0x0B, 0x4A, 0x61, 0x76, 0x61,
				0x43, 0x61, 0x72, 0x64, 0x4F, 0x53, 0x03 };
			//generate RSA 2048 keys pair 
			BYTE APDU_generate_RSA_pair[] = { 0x80, 0x30, 0x01, 0x00, 0x00 };

			// Establish the context.
			lReturn = SCardEstablishContext(
				SCARD_SCOPE_USER,
				NULL,
				NULL,
				&hSC);
			if (ErrorCheck("SCardEstablishContext", lReturn))
				return -1;

			// Retrieve the list the readers.
			lReturn = SCardListReaders(hSC,
				NULL,
				(LPTSTR)&pmszReaders,
				&cch);

			switch (lReturn) {
			case SCARD_E_NO_READERS_AVAILABLE:
				printf("Reader is not in groups.\n");
				return -1;
				break;

			case SCARD_S_SUCCESS:
				// Display print reader.
				printf("Reader: %s\n", pmszReaders);
				break;

			default:
				printf("Failed SCardListReaders\n");
				return -2;
			}

			lReturn = SCardConnect(hSC,
				(LPCTSTR)pmszReaders,
				SCARD_SHARE_SHARED,
				SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1,
				&hCardHandle,
				&dwActiveProtocol);
			if (ErrorCheck("SCardConnect", lReturn))
				return -3;

			// Use the connection.
			// Display the active protocol and define protocol.
			switch (dwActiveProtocol) {
			case SCARD_PROTOCOL_T0:
				printf("Active protocol T0\n");
				pioSendPci = *SCARD_PCI_T0;
				break;

			case SCARD_PROTOCOL_T1:
				printf("Active protocol T1\n");
				pioSendPci = *SCARD_PCI_T1;
				break;

			case SCARD_PROTOCOL_UNDEFINED:
			default:
				printf("Active protocol unnegotiated or unknown\n");
				return -4;
			}

			// Transmit the request.
			// Select app.
			printf("Select RSA keys generating applet:\n");
			dwRecv = sizeof(pbRecv);
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_Select_RSA_applet,
				sizeof(APDU_Select_RSA_applet),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Applet doesn't exist on smartcard!");
				return -5;
			}

			PrintResponse(pbRecv, dwRecv);


			//generate RSA keys pair:
			printf("Generating RSA keys pair:\n");
			dwRecv = sizeof(pbRecv);
			lReturn = SCardTransmit(
				hCardHandle,
				&pioSendPci,
				APDU_generate_RSA_pair,
				sizeof(APDU_generate_RSA_pair),
				NULL,
				pbRecv,
				&dwRecv);
			if (ErrorCheck("SCardTransmit", lReturn)) {
				printf("Error! Unable to generate RSA key pair!");
				return -6;
			}
			PrintResponse(pbRecv, dwRecv);
			///
			// Free the memory.
			lReturn = SCardFreeMemory(
				hSC,
				pmszReaders
			);
			if (ErrorCheck("SCardFreeMemory", lReturn))
				return -7;
			// Free the context.
			SCardReleaseContext(hSC);
			if (ErrorCheck("SCardReleaseContext", lReturn))
				return -8;

			return 0;

		}
		// TODO: Add your methods for this class here.
	};
}
