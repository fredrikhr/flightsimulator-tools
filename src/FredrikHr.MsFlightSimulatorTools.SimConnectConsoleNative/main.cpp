#include <Windows.h>

#include <iostream>

#include "SimConnect.h"

void main(void)
{
    HANDLE hSimConnect;
    HRESULT hr;
    TCHAR msgtext[MAX_PATH];
    hr = SimConnect_Open(&hSimConnect, "TEST", NULL, 0, NULL, 0);
    FormatMessage(0, NULL, hr, 0, msgtext, _countof(msgtext), NULL);
    std::cout << msgtext << std::endl;
}
