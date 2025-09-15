/*
 * Copyright 2024 alweiz
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace SitePlugin
{
    public enum SiteType
    {
        Unknown,
        YouTubeLive,
        NicoLive,
        Twitch
    }

    public interface IValueChanged
    {
        event EventHandler<ValueChangedEventArgs> ValueChanged;
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public object NewValue { get; }
        public object OldValue { get; }

        public ValueChangedEventArgs(object newValue, object oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
    }

    public interface ISiteMessage : IValueChanged
    {
        string Raw { get; }
        SiteType SiteType { get; }
    }

    public interface IUser
    {
        string UserId { get; }
        string Nickname { get; }
    }

    public interface IConnectionStatus
    {
        string SiteName { get; }
        bool IsLoggedIn { get; }
    }

    public interface IMessageMetadata
    {
        bool IsNgUser { get; }
        bool IsInitialComment { get; }
        bool Is184 { get; }
    }
}