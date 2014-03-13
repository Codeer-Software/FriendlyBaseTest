
// MfcTestTarget.h : PROJECT_NAME アプリケーションのメイン ヘッダー ファイルです。
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH に対してこのファイルをインクルードする前に 'stdafx.h' をインクルードしてください"
#endif

#include "resource.h"		// メイン シンボル


// CMfcTestTargetApp:
// このクラスの実装については、MfcTestTarget.cpp を参照してください。
//

class CMfcTestTargetApp : public CWinApp
{
public:
	CMfcTestTargetApp();

// オーバーライド
public:
	virtual BOOL InitInstance();

// 実装

	DECLARE_MESSAGE_MAP()
};

extern CMfcTestTargetApp theApp;