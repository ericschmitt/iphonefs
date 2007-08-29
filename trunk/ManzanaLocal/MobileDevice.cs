// Software License Agreement (BSD License)
// 
// Copyright (c) 2007, Peter Dennis Bartok <PeterDennisBartok@gmail.com>
// Copyright (c) 2007, Lokkju Inc. <lokkju@lokkju.com>
// All rights reserved.
// 
// Redistribution and use of this software in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above
//   copyright notice, this list of conditions and the
//   following disclaimer.
// 
// * Redistributions in binary form must reproduce the above
//   copyright notice, this list of conditions and the
//   following disclaimer in the documentation and/or other
//   materials provided with the distribution.
// 
// * Neither the name of Peter Dennis Bartok nor the names of its
//   contributors may be used to endorse or promote products
//   derived from this software without specific prior
//   written permission of Yahoo! Inc.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

//
// Based on code developed by geohot, ixtli, nightwatch, warren
// See http://iphone.fiveforty.net/wiki/index.php?title=Main_Page
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Manzana
{
	internal enum AppleMobileErrors
	{

	}

	/// <summary>
	/// Provides the fields representing the type of notification
	/// </summary>
	public enum NotificationMessage {
		/// <summary>The iPhone was connected to the computer.</summary>
		Connected		= 1,
		/// <summary>The iPhone was disconnected from the computer.</summary>
		Disconnected	= 2,

		/// <summary>Notification from the iPhone occurred, but the type is unknown.</summary>
		Unknown			= 3,
	}

	/// <summary>
	/// Structure describing the iPhone
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	public struct AMDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		internal byte[]		unknown0;		/* 0 - zero */
		internal uint		device_id;		/* 16 */
		internal uint		product_id;		/* 20 - set to AMD_IPHONE_PRODUCT_ID */
		/// <summary>Write Me</summary>
		public string		serial;			/* 24 - set to AMD_IPHONE_SERIAL */
		internal uint		unknown1;		/* 28 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		internal byte[]		unknown2;		/* 32 */
		internal uint		lockdown_conn;	/* 36 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		internal byte[]		unknown3;		/* 40 */
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotification {
		uint						unknown0;	/* 0 */
		uint						unknown1;	/* 4 */
		uint						unknown2;	/* 8 */
		DeviceNotificationCallback	callback;   /* 12 */ 
		uint						unknown3;	/* 16 */
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotificationCallbackInfo {
		public AMDevice dev {
			get {
				return (AMDevice)Marshal.PtrToStructure(dev_ptr, typeof(AMDevice));
			}
		}
		internal IntPtr	dev_ptr;
		public NotificationMessage msg;
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMRecoveryDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public byte[]	unknown0;			/* 0 */
		public DeviceRestoreNotificationCallback	callback;		/* 8 */
		public IntPtr	user_info;			/* 12 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public byte[]	unknown1;			/* 16 */
		public uint		readwrite_pipe;		/* 28 */
		public byte		read_pipe;          /* 32 */
		public byte		write_ctrl_pipe;    /* 33 */
		public byte		read_unknown_pipe;  /* 34 */
		public byte		write_file_pipe;    /* 35 */
		public byte		write_input_pipe;   /* 36 */
	};

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct afc_directory {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=0)]
		byte[] unknown;   /* size unknown */
	};
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct afc_dictionary
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0)]
        byte[] unknown;   /* size unknown */
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct afc_device_info
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public byte[] unknown;   /* size unknown */
    }

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct afc_connection {
		uint handle;            /* 0 */
		uint unknown0;          /* 4 */
		byte unknown1;         /* 8 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		byte[] padding;			/* 9 */
		uint unknown2;          /* 12 */
		uint unknown3;          /* 16 */
		uint unknown4;          /* 20 */
		uint fs_block_size;     /* 24 */
		uint sock_block_size;   /* 28: always 0x3c */
		uint io_timeout;        /* 32: from AFCConnectionOpen, usu. 0 */
		IntPtr afc_lock;                 /* 36 */
		uint context;           /* 40 */
	};


	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceRestoreNotificationCallback(ref AMRecoveryDevice callback_info);

	internal class MobileDevice {
		public static int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1, uint unused2, uint unused3, ref AMDeviceNotification notification) {
			IntPtr	ptr;
			int		ret;

			ptr = IntPtr.Zero;
			ret = AMDeviceNotificationSubscribe(callback, unused1, unused2, unused3, ref ptr);
			if ((ret == 0) && (ptr != IntPtr.Zero)) {
				notification = (AMDeviceNotification)Marshal.PtrToStructure(ptr, notification.GetType());
			}
			return ret;
		}

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		private extern static int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1, uint unused2, uint unused3, ref IntPtr am_device_notification_ptr);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceConnect(ref AMDevice device);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceIsPaired(ref AMDevice device);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceValidatePairing(ref AMDevice device);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceStartSession(ref AMDevice device);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceGetConnectionID(ref AMDevice device);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMRestoreModeDeviceCreate(uint unknown0, int connection_id, uint unknown1);

        /// <summary>
        /// Pass in a IntPtr. It will be filled.  Use it with the GetKeyValue shite
        /// </summary>
        /// <param name="conn">Pointer to an afc_connection struct</param>
        /// <param name="info">Pointer to an afc_dictionary struct</param>
        /// <returns>afc_error</returns>
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCDeviceInfoOpen(IntPtr conn, ref IntPtr buffer);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int AFCFileInfoOpen(IntPtr conn, string path,out IntPtr buffer);
        [DllImport("iTunesMobileDevice.dll", CallingConvention = CallingConvention.Cdecl)]
		public extern static int AFCDirectoryOpen(IntPtr conn, string path, ref IntPtr dir);

		public static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref string buffer) {
			IntPtr ptr;
			int ret;

			ptr = IntPtr.Zero;
			ret = AFCDirectoryRead(conn, dir, ref ptr);
			if ((ret == 0) && (ptr != IntPtr.Zero)) {
				buffer = Marshal.PtrToStringAnsi(ptr);
			} else {
				buffer = null;
			}
			return ret;
		}
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCDirectoryRead(IntPtr conn, IntPtr dir, ref IntPtr dirent);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCDirectoryClose(IntPtr conn, IntPtr dir);
        
        [DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCKeyValueRead(IntPtr dict, out IntPtr key, out IntPtr value);
        [DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCKeyValueClose(IntPtr dict);
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMRestoreRegisterForDeviceNotifications(
			DeviceRestoreNotificationCallback dfu_connect, 
			DeviceRestoreNotificationCallback recovery_connect, 
			DeviceRestoreNotificationCallback dfu_disconnect,
			DeviceRestoreNotificationCallback recovery_disconnect,
			uint unknown0,
			IntPtr user_info);


		public static int AMDeviceStartService(ref AMDevice device, string service_name, ref afc_connection conn, IntPtr unknown) {
			IntPtr ptr;
			int ret;

			ptr = IntPtr.Zero;
			ret = AMDeviceStartService(ref device, StringToCFString(service_name), ref ptr, unknown);
			if ((ret == 0) && (ptr != IntPtr.Zero)) {
				conn = (afc_connection)Marshal.PtrToStructure(ptr, conn.GetType());
			}
			return ret;
		}
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMDeviceStartService(ref AMDevice device, byte[] service_name, ref IntPtr handle, IntPtr unknown);

		public static int AFCConnectionOpen(IntPtr handle, uint io_timeout, ref afc_connection conn) {
			IntPtr ptr;
			int ret;

			ptr = IntPtr.Zero;
			ret = AFCConnectionOpen(handle, io_timeout, ref ptr);
			if ((ret == 0) && (ptr != IntPtr.Zero)) {
				conn = (afc_connection)Marshal.PtrToStructure(ptr, conn.GetType());
			}
			return ret;
		}
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCConnectionOpen(IntPtr handle, uint io_timeout, ref IntPtr conn);

		public static string AMDeviceCopyValue(ref AMDevice device, uint unknown, string name) {
			IntPtr	result;
			byte[]	cfstring;

			cfstring = StringToCFString(name);

			result = AMDeviceCopyValue_Int(ref device, unknown, cfstring);
			if (result != IntPtr.Zero) {
				byte length;

				length = Marshal.ReadByte(result, 8);
				if (length > 0) {
					return Marshal.PtrToStringAnsi(new IntPtr(result.ToInt64() + 9), length);
				} else {
					return String.Empty;
				}
			}
			return String.Empty;
		}

		[DllImport("iTunesMobileDevice.dll", EntryPoint="AMDeviceCopyValue", CallingConvention=CallingConvention.Cdecl)]
		public extern static IntPtr AMDeviceCopyValue_Int(ref AMDevice device, uint unknown, byte[] cfstring);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCGetFileInfo(IntPtr conn, string path, ref IntPtr buffer, out uint length);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCRemovePath(IntPtr conn, string path);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCRenamePath(IntPtr conn, string old_path, string new_path);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefOpen(IntPtr conn, string path, int mode, int unknown, out Int64 handle);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefClose(IntPtr conn, Int64 handle);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefRead(IntPtr conn, Int64 handle, byte[] buffer, ref uint len);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefWrite(IntPtr conn, Int64 handle, byte[] buffer, uint len);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFlushData(IntPtr conn, Int64 handle);

		// FIXME - not working, arguments? Always returns 7
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefSeek(IntPtr conn, Int64 handle, uint pos, uint origin);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefTell(IntPtr conn, Int64 handle, ref uint position);

		// FIXME - not working, arguments?
		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCFileRefSetFileSize(IntPtr conn, Int64 handle, uint size);

		[DllImport("iTunesMobileDevice.dll", CallingConvention=CallingConvention.Cdecl)]
		public extern static int AFCDirectoryCreate(IntPtr conn, string path);


		internal static byte[] StringToCFString(string value) {
			byte[] b;

			b = new byte[value.Length + 10];
			b[4] = 0x8c;
			b[5] = 07;
			b[6] = 01;
			b[8] = (byte)value.Length;
			Encoding.ASCII.GetBytes(value, 0, value.Length, b, 9);
			return b;
		}

		internal static string CFStringToString(byte[] value) {
			return Encoding.ASCII.GetString(value, 9, value[9]);
		}
	}

}
