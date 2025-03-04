#include "pch.h"
#include "sys.h"
#include <string>


// Define a function type (adjust as needed)
typedef void (*func_t)();
// Global variables for trampoline and original function address
void* trampoline = nullptr;
func_t original_ptr = nullptr;
HWND hwnd = nullptr;
int orgNext = 0;

void send_msg(char* msg, unsigned flag) {
	if (hwnd != nullptr) {
		COPYDATASTRUCT cds;
		cds.dwData = 1; // Custom identifier (can be any value)
		cds.cbData = strlen(msg) + 1; // Size of the string including null terminator
		cds.lpData = (void*)msg; // Pointer to the string data
		// Send the WM_COPYDATA message
		SendMessage(hwnd, WM_COPYDATA, (WPARAM)flag, (LPARAM)&cds);
	}
}

char* msg;
unsigned flag;

void __declspec(naked) override_write_screen_call()
{
	_asm {
		pushad
		mov msg, edx
		mov flag, ecx
	}
	//int len = MultiByteToWideChar(CP_ACP, 0, msg, -1, NULL, 0);
	//wchar_t* utf16Str = new wchar_t[len];
	//MultiByteToWideChar(CP_ACP, 0, msg, -1, utf16Str, len);
	//// 使用MessageBoxW显示消息框
	//MessageBoxW(NULL, utf16Str, L"标题", MB_OK);
	send_msg(msg, flag);
	_asm {
		popad
		push ebp
		mov ebp, esp
		add esp, 0xFFFFFFD8
		push ebx
		push esi
		mov ebx, orgNext
		jmp ebx
	}
	//((func_t)trampoline)();
}

bool hook_address(void* target_address, void* hook_function) {
	original_ptr = (func_t)target_address;
	orgNext = ((int)original_ptr) + 8;
	// Allocate memory for trampoline (size of jump + overwritten bytes)
	trampoline = VirtualAlloc(nullptr, 13, MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
	if (!trampoline) return false;

	// Calculate relative jump addresses
	DWORD hook_offset = (DWORD)((char*)hook_function - (char*)target_address - 5);
	DWORD return_offset = (DWORD)((char*)target_address + 5 - ((char*)trampoline + 10));

	// Save original bytes and create jump in trampoline
	memcpy(trampoline, target_address, 8);
	*(BYTE*)((char*)trampoline + 8) = 0xE9; // jmp
	*(DWORD*)((char*)trampoline + 9) = return_offset;

	// Overwrite original function with jump to hook
	DWORD old_protect;
	VirtualProtect(target_address, 5, PAGE_EXECUTE_READWRITE, &old_protect);
	*(BYTE*)target_address = 0xE9; // jmp
	*(DWORD*)((char*)target_address + 1) = hook_offset;
	VirtualProtect(target_address, 5, old_protect, &old_protect);
	return true;
}

void restore_write_screen_call() {
	if (original_ptr == nullptr || trampoline == nullptr) {
		return;
	}
	DWORD old_protect;
	VirtualProtect(original_ptr, 8, PAGE_EXECUTE_READWRITE, &old_protect);
	//memcpy(original_ptr, trampoline, 8);
	char org[8] = { 0x55, 0x8b,0xec,0x83,0xc4,0xd8 ,0x53,0x56 };
	memcpy(original_ptr, org, 8);
	//memcpy(original_ptr, trampoline, 5);
	VirtualProtect(original_ptr, 8, old_protect, &old_protect);
}


void any_call(int* data) {
	void (*func)() = reinterpret_cast<void(*)()>(data);
	func();
}

void Sys::process(int code, int* data)
{
	switch (code)
	{
	case 9001: //hook 写屏call
		hwnd = (HWND)data[1];
		hook_address((void*)data[0], override_write_screen_call);
		break;
	case 9002: //恢复写屏call
		restore_write_screen_call();
		break;

	case 9999: //执行任务ASM代码
		any_call(data);
		break;

	default:
		break;

	}
}