#pragma once


// CPropertyChild ダイアログ

class CPropertyChild : public CPropertyPage
{
	DECLARE_DYNAMIC(CPropertyChild)

public:
	CPropertyChild();
	virtual ~CPropertyChild();

// ダイアログ データ
	enum { IDD = IDD_DIALOG_PROPERTY_CHILD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV サポート

	DECLARE_MESSAGE_MAP()
};
