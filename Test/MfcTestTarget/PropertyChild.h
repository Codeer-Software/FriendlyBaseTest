#pragma once


// CPropertyChild �_�C�A���O

class CPropertyChild : public CPropertyPage
{
	DECLARE_DYNAMIC(CPropertyChild)

public:
	CPropertyChild();
	virtual ~CPropertyChild();

// �_�C�A���O �f�[�^
	enum { IDD = IDD_DIALOG_PROPERTY_CHILD };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV �T�|�[�g

	DECLARE_MESSAGE_MAP()
};
