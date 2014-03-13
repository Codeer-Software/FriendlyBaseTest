// PropertyChild.cpp : 実装ファイル
//

#include "stdafx.h"
#include "MfcTestTarget.h"
#include "PropertyChild.h"
#include "afxdialogex.h"


// CPropertyChild ダイアログ

IMPLEMENT_DYNAMIC(CPropertyChild, CPropertyPage)

CPropertyChild::CPropertyChild()
	: CPropertyPage(CPropertyChild::IDD)
{

}

CPropertyChild::~CPropertyChild()
{
}

void CPropertyChild::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CPropertyChild, CPropertyPage)
END_MESSAGE_MAP()


// CPropertyChild メッセージ ハンドラー
