/*--------------------------------------------------------------------*\
 * This source file is subject to the GPLv3 license that is bundled   *
 * with this package in the file COPYING.                             *
 * It is also available through the world-wide-web at this URL:       *
 * http://www.gnu.org/licenses/gpl-3.0.txt                            *
 * If you did not receive a copy of the license and are unable to     *
 * obtain it through the world-wide-web, please send an email         *
 * to bsd-license@lokkju.com so we can send you a copy immediately.   *
 *                                                                    *
 * @category   iPhone                                                 *
 * @package    iPhone File System for Windows                         *
 * @copyright  Copyright (c) 2010 Lokkju Inc. (http://www.lokkju.com) *
 * @license    http://www.gnu.org/licenses/gpl-3.0.txt GNU v3 Licence *
 *                                                                    *
 * $Revision::                            $:  Revision of last commit *
 * $Author::                              $:  Author of last commit   *
 * $Date::                                $:  Date of last commit     *
 * $Id::                                                            $ *
\*--------------------------------------------------------------------*/
/*
 * This file is based on work under the following copyright and permission
 * notice:
// Software License Agreement (BSD License)
// 
// Copyright (c) 2010, Lokkju Inc. <lokkju@lokkju.com>
// Copyright (c) 2007, Peter Dennis Bartok <PeterDennisBartok@gmail.com>
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
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Manzana
{
	/// <summary>
	/// Provides data for the <see>DfuConnect</see>, <see>DfuDisconnect</see>, <see>RecoveryModeEnter</see> and <see>RecoveryModeLeave</see> events.
	/// </summary>
	public class DeviceNotificationEventArgs : EventArgs {
		private AMRecoveryDevice	device;

		internal DeviceNotificationEventArgs(AMRecoveryDevice device) {
			this.device = device;
		}

		internal AMRecoveryDevice Device {
			get {
				return device;
			}
		}
	}
}