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

using SitePlugin;
using System;
using System.Collections.Generic;

namespace YouTubeLiveSitePlugin
{
    public enum YouTubeLiveMessageType
    {
        Unknown,
        Comment,
        SuperChat,
        SuperSticker,
        Membership,
        Connected,
        Disconnected
    }

    public interface IYouTubeLiveComment : ISiteMessage
    {
        string Id { get; }
        string UserId { get; }
        DateTime PostedAt { get; }
        IEnumerable<IMessagePart> NameItems { get; }
        IEnumerable<IMessagePart> CommentItems { get; }
        IMessageImage UserIcon { get; }
        YouTubeLiveMessageType YouTubeLiveMessageType { get; }
    }

    public interface IYouTubeLiveConnected : ISiteMessage
    {
        string Text { get; }
        YouTubeLiveMessageType YouTubeLiveMessageType { get; }
    }

    public interface IYouTubeLiveDisconnected : ISiteMessage
    {
        string Text { get; }
        YouTubeLiveMessageType YouTubeLiveMessageType { get; }
    }

    public interface IMessagePart
    {
        string Text { get; }
    }

    public interface IMessageImage
    {
        string Url { get; }
    }
}