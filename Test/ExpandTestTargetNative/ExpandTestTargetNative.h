// ExpandTestTargetNative.h : ExpandTestTargetNative.DLL �̃��C�� �w�b�_�[ �t�@�C��
//

#pragma once

#ifndef __AFXWIN_H__
	#error "PCH �ɑ΂��Ă��̃t�@�C�����C���N���[�h����O�� 'stdafx.h' ���C���N���[�h���Ă�������"
#endif

#include "resource.h"		// ���C�� �V���{��


// CExpandTestTargetNativeApp
// ���̃N���X�̎����Ɋւ��Ă� ExpandTestTargetNative.cpp ���Q�Ƃ��Ă��������B
//

class CExpandTestTargetNativeApp : public CWinApp
{
public:
	CExpandTestTargetNativeApp();

// �I�[�o�[���C�h
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
