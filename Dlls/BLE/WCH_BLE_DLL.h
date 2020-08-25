//****************************************
//**  Copyright  (C)  WCH 2019   **
//**  Web:  http://www.wch.cn  **
//****************************************
//**  DLL for BLE  **
//**  Win10 ,C/C++ **
//****************************************
//
// BLE接口库 Version：1.0.0
// 沁恒微电子  作者: WCH—PK 2019.10
// WCH_BLE_DLL  V1.0 
// 运行环境: Windows 10
#pragma once
#include "stdafx.h"

#ifdef __cplusplus
extern "C" {
#endif

//
// 摘要:
//     回调，通知应用上层BLE操作已成功和相关信息
//
// 参数:
//   uAction_Service:
//     BLE操作的序号：
//	   0:建立通讯	
//	   1:建立蓝牙设备连接
//	   2:获取服务
//	   3:获取特征
//	   4:查看特征支持的操作
//	   5:无
//	   6:写操作
//	   7:打开订阅
//	   8:关闭订阅
//	   9:释放资源
//	   10:传输设备ID值
//	   11:无
//	   12:关闭后台通讯
//   tmpStr:
//     后台操作的结果。
//   iExtraInfo:
//     1.指定特征值所具有的操作序号(Read， Write， Subscribe)：0-7。仅用在枚举特征值功能时
//
// 返回结果:
//     无。
typedef void (CALLBACK* Notify_AppService)(ULONG uAction_Service, PCHAR tmpStr, INT iExtraInfo);
//
// 摘要:
//     回调，通知应用上层DeviceWatcher的操作结果
//
// 参数:
//   deviceName:
//     枚举到的设备名称。
//   deviceID:
//     枚举到的设备ID。
//
// 返回结果:
//     无。
typedef void (CALLBACK* Notify_DeviceWatcher)(PCHAR deviceName, PCHAR deviceID);
//
// 摘要:
//     回调，通知应用上层AppService出现异常的值
//     包括了：BLE操作的异常；桌面应用和后台任务通讯间的异常
//
// 参数:
//   errorString:
//     异常的属性。
//
// 返回结果:
//     无。
typedef void (CALLBACK* Notify_ErrorStatus)(PCHAR errorString);
//
// 摘要:
//     回调，返回读取的字节
//
// 参数:
//   readBytes:
//     读取的字节。
//   iLength:
//     字节数。
//
// 返回结果:
//     无。
typedef void (CALLBACK* Notify_ReadBytes)(PUCHAR readBytes, INT iLength);
//
// 摘要:
//     第一步：连接后台UWP程序，通过回调判断连接结果。
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEConnectToService(Notify_AppService fun, Notify_ErrorStatus errorFun, Notify_ReadBytes readFun);
//
// 摘要:
//     第二步：枚举周围BLE设备，通过回调Notify_DeviceWatcher返回枚举结果。
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEStartScanBLEDevices(Notify_DeviceWatcher fun);
//
// 摘要:
//     第三步：结束枚举，释放资源。
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEStopScanBLEDevices(Notify_DeviceWatcher fun);
//
// 摘要:
//     第四步：选择待连接设备，将设备ID发送给后台UWP程序
//
// 参数:
//	   回调函数。
//   deviceID:
//		设备的ID字符串。
//
// 返回结果:
//     无。
void WINAPI WCHBLESendBLEDeviceID(PCHAR deviceID, Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第五步：连接指定的BLE设备
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLECreateConnection(Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第六步：获取指定BLE支持的服务列表，以字符串形式通过回调返回上层应用
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEGetServicesEnum(Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第七步：获取指定服务所支持的特征列表，以字符串形式通过回调返回上层应用
//
// 参数:
//	   回调函数。
//   nService:
//		指定的服务序号。
//
// 返回结果:
//     无。
void WINAPI WCHBLEGetCharacteristicEnum(INT nService, Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第八步：获取指定特征所支持的操作，回调返回0-7，按位表示具有的功能(低位-高位：read, write, subscribe)
//	   0			0			0
//	 Subscribe    Write		   Read
//
// 参数:
//	   回调函数。
//   nCharacteristic:
//	   选定的特征项。
//
// 返回结果:
//     无。
void WINAPI WCHBLEGetCharacteristicAction(INT nCharacteristic, Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第九步：读取特征值。
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEGetReadBuffer(Notify_ReadBytes fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十步：写入特征值。
//
// 参数:
//	   回调函数。
//   bufferStr:
//	   待写入的数组。
//
// 返回结果:
//     无。
void WINAPI WCHBLEWriteBuffer(PCHAR bufferStr, Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十步Ex：写入特征值。
//
// 参数:
//	   回调函数。
//   bufferStr:
//	   待写入的数组。
//
// 返回结果:
//     无。
void WINAPI WCHBLEWriteBufferEx(PCHAR bufferStr, UINT length, Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十一步：开启ValueChanged事件订阅，每当特征值改变时，获取特征值
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEStartMonitoring(Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十二步：关闭ValueChanged事件订阅
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEStopMonitoring(Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十三步：释放资源，关闭与BLE设备的连接
//
// 参数:
//	   回调函数。
//
// 返回结果:
//     无。
void WINAPI WCHBLEReleaseResource(Notify_AppService fun, Notify_ErrorStatus errorFun);
//
// 摘要:
//     第十四步：释放资源，关闭与后台UWP的连接
//
// 参数:
//	   无。
//
// 返回结果:
//     无。
void WINAPI WCHBLECloseConnection();

#ifdef __cplusplus
}
#endif