#include "gfx.h"

void ll_add_next(LLITEM **existing, LLITEM *new) {
	if (*existing) {
		if ((*existing)->next) {
			(*existing)->next->prev = new;
		}

		new->next = (*existing)->next;
		new->prev = *existing;

		(*existing)->next = new;
	} else {
		*existing = new;
		new->next = 0;
		new->prev = 0;
	}
}

void ll_rem(LLITEM *item) {
	if (item->prev) {
		item->prev->next = item->next;
	}

	if (item->next) {
		item->next->prev = item->prev;
	}
}