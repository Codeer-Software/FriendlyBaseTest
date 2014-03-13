// ExpandTestTargetNative.h : ExpandTestTargetNative.DLL のメイン ヘッダー ファイル
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CExpandTestTargetNativeApp
// このクラスの実装に関しては ExpandTestTargetNative.cpp を参照してください。
//

class CExpandTestTargetNativeApp : public CWinApp
{
public:
	CExpandTestTargetNativeApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
